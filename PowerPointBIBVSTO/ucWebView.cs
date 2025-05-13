using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using PowerPointAnalyzer;


namespace PowerPointBIBVSTO
{
    public partial class ucWebView : UserControl
    {
        private readonly WebView2 _webView;
        private const string DefaultUrl = "https://agreeable-beach-05622ca10.6.azurestaticapps.net/taskpane.html";
        private ucWebView _webViewControl;
        

        public ucWebView()
        {
            InitializeComponent();

            _webView = new WebView2
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_webView);

            Load += OnLoadAsync;
        }

        public void Navigate(string url = DefaultUrl)
        {
            if (_webView.CoreWebView2 != null)
                _webView.CoreWebView2.Navigate(url);
            else
                _webView.Source = new Uri(url);
        }

        private async void OnLoadAsync(object sender, EventArgs e)
        {
            try
            {
                await EnsureWebView2Async();
                _webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
                Navigate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo inicializar WebView2.\n" + ex.Message,
                    "Panel React",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task EnsureWebView2Async()
        {
            if (_webView.CoreWebView2 != null)
                return;

            try
            {
                string tempProfile = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(), "MyWebView2Profile");

                var env = await CoreWebView2Environment.CreateAsync(null, tempProfile);
                await _webView.EnsureCoreWebView2Async(env);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creando entorno WebView2:\n" + ex.Message,
                    "WebView2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // 🟦 Ejecutar JavaScript desde VSTO → React
        public void ExecuteScript(string js)
        {
            try
            {
                _webView?.ExecuteScriptAsync(js);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar script JS:\n" + ex.Message);
            }
        }

        // 🟩 Recibir mensajes desde React → VSTO
        private void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string rawMessage = e.WebMessageAsJson;

                // Puedes mantener esto activo para depuración:
                // MessageBox.Show("📨 Mensaje recibido:\n" + rawMessage);

                var webMessage = JsonConvert.DeserializeObject<WebViewMessage>(rawMessage);
                string rawAction = webMessage?.action ?? string.Empty;
                string action = rawAction.Trim().ToLowerInvariant();

                switch (action)
                {
                    case "fixissue":
                        {
                            var fixData = webMessage.data?.ToObject<Dictionary<string, object>>();
                            if (fixData != null)
                                Globals.ThisAddIn.HandleFixIssue(fixData);
                        }
                        break;

                    case "selectslide":
                        {
                            var selectData = webMessage.data?.ToObject<Dictionary<string, object>>();
                            if (selectData != null)
                                Globals.ThisAddIn.HandleSelectSlide(selectData);
                        }
                        break;

                    case "senddummy":
                        Globals.ThisAddIn.SendDummyDataToReact();
                        break;

                    case "getactiveslideinfo":
                        Globals.ThisAddIn.HandleReactRequestSlideInfo();
                        break;

                    case "refreshactivepresentation":
                        // Puedes implementar esto si deseas refrescar algo
                        break;

                    default:
                        MessageBox.Show("❗ Acción desconocida desde React: " + rawAction);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al procesar mensaje desde React:\n" + ex.Message);
            }
        }

        public void SendSlideInfoFromReader(SlideReader reader)
        {
            try
            {
                var slideInfo = reader.GetActiveSlideInfo(); // Usa la lógica que ya tienes
                var payload = new
                {
                    action = "slideInfoReceived",
                    data = slideInfo
                };

                string json = JsonConvert.SerializeObject(payload);
                _webView.CoreWebView2.PostWebMessageAsJson(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error enviando SlideInfo a React:\n" + ex.Message);
            }
        }
        public void SendAllSlidesInfoToReact(List<SlideInfo> slidesInfo)
        {
            try
            {
                var payload = new
                {
                    action = "allSlidesReceived",
                    data = slidesInfo // ← NO SERIALICES AÚN
                };

                string json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                });

                // Solo para depurar (ahora será legible y correcto)
                MessageBox.Show("Mensaje a enviar:\n\n" + json);

                _webView.CoreWebView2.PostWebMessageAsJson(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar las diapositivas a React:\n" + ex.Message);
            }
        }

        public void SendErrorToReact(string message)
        {
            var payload = new
            {
                action = "error",
                data = new { message }
            };

            string json = JsonConvert.SerializeObject(payload);
            _webView.CoreWebView2.PostWebMessageAsJson(json);
        }


        // 📦 Estructura de mensaje
        public class WebViewMessage
        {
            public string action { get; set; }
            public JObject data { get; set; }
        }
    }
}
