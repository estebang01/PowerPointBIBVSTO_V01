using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Tools;
using Microsoft.Office.Tools.Ribbon;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using Newtonsoft.Json;
using static PowerPointBIBVSTO.ThisAddIn.SlideIssue;
using PowerPointAnalyzer;

namespace PowerPointBIBVSTO
{
    public partial class ThisAddIn
    {
        public ucWebView WebViewControl
        {
            get => _webViewControl;
        }
        /* ───────── Modelos ───────── */
        public class SlideIssue
        {
            public int id { get; set; }
            public string type { get; set; } // "error", "warning", "info"
            public string title { get; set; }
            public string description { get; set; }
            public string slideId { get; set; }
            public string elementId { get; set; }
            public bool fixed_ { get; set; }

            public bool isFixed
            {
                get => fixed_;
                set => fixed_ = value;
            }
            public class SlideData
            {
                public string id { get; set; }
                public string title { get; set; }
                public List<SlideIssue> issues { get; set; }
            }
        }
        /* ───────── Campos ───────── */
        private CustomTaskPane _slidePickerPane;
        private CustomTaskPane _reactPane;
        private ucSlidePicker _picker;
        private ucWebView _webViewControl;

        /* ───────── Ribbon ───────── */
        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return Globals.Factory.GetRibbonFactory()
                                  .CreateRibbonManager(new IRibbonExtension[] { new RibbonBIB() });
        }

        /* ───────── STARTUP ───────── */
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            HideDesignIdeas();
            CreateSlidePickerPane();
            CreateReactPane(); // Carga WebView2 apuntando a Azure
        }
        /* ───────── SHUTDOWN ──────── */
        private void ThisAddIn_Shutdown(object sender, EventArgs e) { }

        /* =================================================================== */
        /*                     Panel React (WebView2)                          */
        /* =================================================================== */

        private void CreateReactPane()
    {
        _webViewControl = new ucWebView(); // Guarda la referencia
        _reactPane = this.CustomTaskPanes.Add(_webViewControl, "Panel BIB");
        _reactPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
        _reactPane.Width = 420;
        _reactPane.Visible = false;
    }

        public void ToggleReactPane()
        {
            _reactPane.Visible = !_reactPane.Visible;

            if (_reactPane.Visible)
            {
                AnalyzeCurrentPresentation(); // puedes llamarlo aquí si ya lo tienes
            }
        }

        /* =================================================================== */
        /*                         Slide Picker                                */
        /* =================================================================== */

        private void CreateSlidePickerPane()
        {
            _picker = new ucSlidePicker();
            _slidePickerPane = this.CustomTaskPanes.Add(_picker, "Biblioteca de Slides");
            _slidePickerPane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
            _slidePickerPane.Width = 350;
            _slidePickerPane.Visible = false;
        }

        public void MostrarSlidePicker()
        {
            using (var dlg = new FolderBrowserDialog
            {
                Description = "Seleccione la carpeta que contiene las presentaciones maestro (.pptx)",
                ShowNewFolderButton = false
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string[] archivos = Directory.GetFiles(dlg.SelectedPath, "*.pptx");
                if (archivos.Length == 0)
                {
                    MessageBox.Show("La carpeta seleccionada no contiene archivos .pptx.",
                                    "Slide Picker",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                _picker.LoadTemplates(archivos);
                _slidePickerPane.Visible = true;
            }
        }

        /* =================================================================== */
        /*                         Utilidades                                  */
        /* =================================================================== */

        private void HideDesignIdeas()
        {
            try
            {
                var bar = Application.CommandBars["Design Ideas"];
                if (bar != null) bar.Visible = false;
            }
            catch { /* versiones de PPT sin este CommandBar */ }
        }

/* ───────── VSTO wiring ──────── */
        private void InternalStartup()
        {
            this.Startup += ThisAddIn_Startup;
            this.Shutdown += ThisAddIn_Shutdown;
        }
        private int _issueIdCounter = 1;
        public void SendDataToReact(object data)
        {
            try
            {
                string json;
                if (data is string s)
                {
                    json = s;
                }
                else
                {
                    // Si data es un objeto, lo convertimos a JSON
                    json = JsonConvert.SerializeObject(data);
                }
                string script = $"window.initializeDataFromVSTO({json});";
                _webViewControl.ExecuteScript(script);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar datos a React:\n" + ex.Message);
            }
        }

        public void HandleFixIssue(object data)
        {
            try
            {
                /* var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.ToString());

                string slideId = dict["slideId"] as string;
                int issueId = Convert.ToInt32(dict["issueId"]);
                bool willBeFixed = Convert.ToBoolean(dict["willBeFixed"]);
                string type = dict["type"] as string;
                string elementId = dict["elementId"] as string;

                var presentation = Application.ActivePresentation;
                foreach (PowerPoint.Slide slide in presentation.Slides)
                {
                    if ("slide_" + slide.SlideID != slideId)
                        continue;

                    if (type == "error" && elementId == "title")
                    {
                        foreach (PowerPoint.Shape shape in slide.Shapes)
                        {
                            if (shape.Type == Office.MsoShapeType.msoPlaceholder &&
                                shape.PlaceholderFormat.Type == PowerPoint.PpPlaceholderType.ppPlaceholderTitle &&
                                willBeFixed)
                            {
                                shape.TextFrame.TextRange.Text = "Título automático";
                            }
                        }
                    }
                    else if (type == "warning" && elementId.StartsWith("shape_"))
                    {
                        int shapeId = int.Parse(elementId.Substring(6));
                        foreach (PowerPoint.Shape shape in slide.Shapes)
                        {
                            if (shape.Id == shapeId && shape.HasTextFrame == Office.MsoTriState.msoTrue)
                            {
                                shape.TextFrame2.AutoSize = willBeFixed
                                    ? Office.MsoAutoSize.msoAutoSizeNone
                                    : Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                            }
                        }
                    }
                }

                // Reanalizar después de aplicar corrección
                AnalyzeCurrentPresentation();*/
                MessageBox.Show("✅ Entró a HandleFixIssue con datos:\n" + JsonConvert.SerializeObject(data));
                var response = new { message = "Fix applied", timestamp = DateTime.Now };
                SendDataToReact(response); // Esto llama a _webViewControl.ExecuteScript(...)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en HandleFixIssue:\n" + ex.Message);
            }
        }

        public void AnalyzeCurrentPresentation()
        {
            try
            {
                var presentation = Application.ActivePresentation;
                if (presentation == null) return;

                var slidesData = new List<SlideData>();
                _issueIdCounter = 1;

                foreach (PowerPoint.Slide slide in presentation.Slides)
                {
                    string slideId = "slide_" + slide.SlideID;
                    var issues = new List<SlideIssue>();

                    bool hasTitle = false;
                    bool hasTitleText = false;

                    foreach (PowerPoint.Shape shape in slide.Shapes)
                    {
                        if (shape.Type == Office.MsoShapeType.msoPlaceholder &&
                            shape.PlaceholderFormat.Type == PowerPoint.PpPlaceholderType.ppPlaceholderTitle)
                        {
                            hasTitle = true;
                            if (shape.HasTextFrame == Office.MsoTriState.msoTrue &&
                                !string.IsNullOrWhiteSpace(shape.TextFrame.TextRange.Text))
                            {
                                hasTitleText = true;
                            }
                        }

                        if (shape.HasTextFrame == Office.MsoTriState.msoTrue)
                        {
                            try
                            {
                                var tf2 = shape.TextFrame2;
                                if (tf2.AutoSize == Office.MsoAutoSize.msoAutoSizeTextToFitShape)
                                {
                                    issues.Add(new SlideIssue
                                    {
                                        id = _issueIdCounter++,
                                        type = "warning",
                                        title = "Shrink text on overflow",
                                        description = "Shrink text está activado.",
                                        slideId = slideId,
                                        elementId = "shape_" + shape.Id,
                                        isFixed = false
                                    });
                                }
                            }
                            catch { /* Algunos shapes no soportan TextFrame2 */ }
                        }
                    }

                    if (hasTitle && !hasTitleText)
                    {
                        issues.Add(new SlideIssue
                        {
                            id = _issueIdCounter++,
                            type = "error",
                            title = "Título vacío",
                            description = "El placeholder de título no tiene texto.",
                            slideId = slideId,
                            elementId = "title",
                            isFixed = false
                        });
                    }

                    slidesData.Add(new SlideData
                    {
                        id = slideId,
                        title = GetSlideTitle(slide),
                        issues = issues
                    });
                }

                SendDataToReact(slidesData); // 👈 Enviar los datos a React
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al analizar la presentación: " + ex.Message);
            }
        }

        private string GetSlideTitle(PowerPoint.Slide slide)
        {
            try
            {
                foreach (PowerPoint.Shape shape in slide.Shapes)
                {
                    if (shape.Type == Office.MsoShapeType.msoPlaceholder &&
                        shape.PlaceholderFormat.Type == PowerPoint.PpPlaceholderType.ppPlaceholderTitle &&
                        shape.HasTextFrame == Office.MsoTriState.msoTrue)
                    {
                        return shape.TextFrame.TextRange.Text?.Trim() ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el título de la diapositiva:\n" + ex.Message);
            }

            return "Slide " + slide.SlideIndex;
        }

        public void HandleSelectSlide(object data)
        {
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.ToString());
                string slideId = dict["slideId"] as string;

                var presentation = Application.ActivePresentation;
                foreach (PowerPoint.Slide slide in presentation.Slides)
                {
                    if ("slide_" + slide.SlideID == slideId)
                    {
                        slide.Select();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en HandleSelectSlide:\n" + ex.Message);
            }
        }

        /* =================================================================== */
        /*                         Prueba Dummy                                */
        /* =================================================================== */
        public void SendDummyDataToReact()
        {
            var dummy = new[]
            {
        new { slide = 1, shape = "Title 1", text = "Bienvenidos" },
        new { slide = 2, shape = "Subtitle", text = "Esto es una demo" },
        new { slide = 2, shape = "ContentBox", text = "Texto de ejemplo" }
    };

            SendDataToReact(dummy);
        }

        /* =================================================================== */
        /*                         Prueba Figuras                                */
        /* =================================================================== */
        public void HandleReactRequestSlideInfo()
        {
            try
            {
                var slideReader = new SlideReader(Application);
                var allSlidesInfo = slideReader.GetAllSlidesInfo();
                WebViewControl.SendAllSlidesInfoToReact(allSlidesInfo);

                string json = JsonConvert.SerializeObject(allSlidesInfo, Formatting.Indented);

                // Mostrar el JSON en un MessageBox (solo los primeros 1000 caracteres por seguridad)
                string preview = json.Length > 1000 ? json.Substring(0, 1000) + "..." : json;
                MessageBox.Show("📦 JSON a enviar a React:\n\n" + preview, "Vista previa del payload");
            }
            catch (Exception ex)
            {
                WebViewControl.SendErrorToReact("❌ Error al analizar las diapositivas:\n" + ex.Message);
            }
        }




    }
}
