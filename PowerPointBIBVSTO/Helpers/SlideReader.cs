using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PowerPointShape = Microsoft.Office.Interop.PowerPoint.Shape;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace PowerPointAnalyzer
{
    /// <summary>
    /// Clase que analiza los elementos de las diapositivas de PowerPoint
    /// </summary>
    public class SlideReader
    {
        private readonly Application _application;
        private Presentation _activePresentation;

        /// <summary>
        /// Inicializa una nueva instancia del analizador de elementos
        /// </summary>
        /// <param name="application">La aplicación PowerPoint activa</param>
        public SlideReader(Application application)
        {
            _application = application;
            _activePresentation = application.ActivePresentation;
        }

        /// <summary>
        /// Actualiza la referencia a la presentación activa
        /// </summary>
        public void RefreshActivePresentation()
        {
            _activePresentation = _application.ActivePresentation;
        }

        /// <summary>
        /// Obtiene información de la diapositiva activa
        /// </summary>
        /// <returns>Información de la diapositiva activa</returns>
        public SlideInfo GetActiveSlideInfo()
        {
            if (_application.ActiveWindow.ViewType != PpViewType.ppViewSlide)
                throw new InvalidOperationException("No hay una vista de diapositiva activa.");

            if (_application.ActiveWindow.Selection.SlideRange.Count == 0)
                throw new InvalidOperationException("No hay diapositiva seleccionada.");

            Slide activeSlide = _application.ActiveWindow.Selection.SlideRange[1];
            return AnalyzeSlide(activeSlide);
        }
        public List<SlideInfo> GetAllSlidesInfo()
        {
            if (_application.Presentations.Count == 0)
                throw new InvalidOperationException("No hay presentaciones abiertas.");

            var presentation = _application.ActivePresentation;

            if (presentation.Slides.Count == 0)
                throw new InvalidOperationException("La presentación no tiene diapositivas.");

            var slidesInfo = new List<SlideInfo>();

            foreach (PowerPoint.Slide slide in presentation.Slides)
            {
                slidesInfo.Add(AnalyzeSlide(slide));
            }

            return slidesInfo;
        }


        /// <summary>
        /// Analiza una diapositiva específica
        /// </summary>
        /// <param name="slide">La diapositiva a analizar</param>
        /// <returns>Información detallada de la diapositiva</returns>
        private SlideInfo AnalyzeSlide(Slide slide)
        {
            SlideInfo slideInfo = new SlideInfo
            {
                SlideNumber = slide.SlideIndex,
                SlideTitle = GetSlideTitle(slide),
                Width = _activePresentation.PageSetup.SlideWidth,
                Height = _activePresentation.PageSetup.SlideHeight,
                Elements = new List<ElementInfo>()
            };

            // Analiza todos los elementos (shapes) en la diapositiva
            foreach (PowerPointShape shape in slide.Shapes)
            {
                ElementInfo elementInfo = AnalyzeElement(shape);
                slideInfo.Elements.Add(elementInfo);
            }

            return slideInfo;
        }

        /// <summary>
        /// Obtiene el título de la diapositiva
        /// </summary>
        private string GetSlideTitle(Slide slide)
        {
            foreach (PowerPointShape shape in slide.Shapes)
            {
                if (shape.Type == MsoShapeType.msoPlaceholder)
                {
                    if (shape.PlaceholderFormat.Type == PpPlaceholderType.ppPlaceholderTitle)
                    {
                        if (shape.HasTextFrame == MsoTriState.msoTrue)
                        {
                            return shape.TextFrame.TextRange.Text;
                        }
                    }
                }
            }
            return string.Empty; // Sin título
        }

        /// <summary>
        /// Analiza un elemento (Shape) de PowerPoint
        /// </summary>
        /// <param name="shape">El elemento a analizar</param>
        /// <returns>Información detallada del elemento</returns>
        private ElementInfo AnalyzeElement(PowerPointShape shape)
        {
            ElementInfo element = new ElementInfo
            {
                Id = shape.Id,
                Name = shape.Name,
                Type = shape.Type.ToString(),
                Left = shape.Left,
                Top = shape.Top,
                Width = shape.Width,
                Height = shape.Height,
                ZOrderPosition = shape.ZOrderPosition
            };

            // Analizar relleno
            if (shape.Fill.Visible == MsoTriState.msoTrue)
            {
                element.Fill = new FillInfo
                {
                    Type = shape.Fill.Type.ToString()
                };

                if (shape.Fill.Type == MsoFillType.msoFillSolid)
                {
                    try
                    {
                        element.Fill.Color = ColorTranslator.FromOle(shape.Fill.ForeColor.RGB).ToArgb().ToString("X");
                        element.Fill.Transparency = shape.Fill.Transparency;
                    }
                    catch { /* Ignorar errores de color */ }
                }
            }

            // Analizar borde
            if (shape.Line.Visible == MsoTriState.msoTrue)
            {
                element.Line = new LineInfo
                {
                    Weight = shape.Line.Weight,
                    Style = shape.Line.Style.ToString()
                };

                try
                {
                    element.Line.Color = ColorTranslator.FromOle(shape.Line.ForeColor.RGB).ToArgb().ToString("X");
                    element.Line.Transparency = shape.Line.Transparency;
                }
                catch { /* Ignorar errores de color */ }
            }

            // Analizar texto
            if (shape.HasTextFrame == MsoTriState.msoTrue)
            {
                TextRange textRange = shape.TextFrame.TextRange;
                element.Text = new TextInfo
                {
                    Content = textRange.Text,
                    WordCount = CountWords(textRange.Text),
                    CharacterCount = textRange.Length
                };

                // Analizar formato de texto
                if (textRange.Length > 0)
                {
                    element.Text.Format = AnalyzeTextFormat(textRange);
                    element.Text.Paragraphs = AnalyzeParagraphs(textRange);
                }
            }

            return element;
        }

        /// <summary>
        /// Analiza el formato del texto
        /// </summary>
        private TextFormatInfo AnalyzeTextFormat(TextRange textRange)
        {
            TextFormatInfo format = new TextFormatInfo
            {
                Font = textRange.Font.Name,
                Size = textRange.Font.Size,
                Bold = textRange.Font.Bold == MsoTriState.msoTrue,
                Italic = textRange.Font.Italic == MsoTriState.msoTrue,
                Underline = textRange.Font.Underline == MsoTriState.msoTrue
            };

            try
            {
                format.Color = ColorTranslator.FromOle(textRange.Font.Color.RGB).ToArgb().ToString("X");
            }
            catch { /* Ignorar errores de color */ }

            return format;
        }

        /// <summary>
        /// Analiza los párrafos del texto
        /// </summary>
        private List<ParagraphInfo> AnalyzeParagraphs(TextRange textRange)
        {
            List<ParagraphInfo> paragraphs = new List<ParagraphInfo>();

            try
            {
                for (int i = 1; i <= textRange.Paragraphs().Count; i++)
                {
                    TextRange paragraph = textRange.Paragraphs(i);

                    ParagraphInfo paragraphInfo = new ParagraphInfo
                    {
                        Text = paragraph.Text.TrimEnd('\r', '\n'),
                        Alignment = paragraph.ParagraphFormat.Alignment.ToString(),
                        IndentLevel = paragraph.IndentLevel
                    };

                    paragraphs.Add(paragraphInfo);
                }
            }
            catch { /* Ignorar errores de párrafos */ }

            return paragraphs;
        }

        /// <summary>
        /// Cuenta las palabras en un texto
        /// </summary>
        private int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            return text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Convierte la información de la diapositiva a formato JSON
        /// </summary>
        /// <paramn ame="slideInfo">Información de la diapositiva</param>
        /// <returns>Representación JSON de la información</returns>
        public string ToJson(SlideInfo slideInfo)
        {
            return JsonSerializer.Serialize(slideInfo, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
    /// Información de una diapositiva
    public class SlideInfo
    {
        public int SlideNumber { get; set; }
        public string SlideTitle { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public List<ElementInfo> Elements { get; set; }
    }

    /// Información de un elemento (Shape)
    public class ElementInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Left { get; set; }
        public float Top { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int ZOrderPosition { get; set; }
        public FillInfo Fill { get; set; }
        public LineInfo Line { get; set; }
        public TextInfo Text { get; set; }
    }

    /// Información del relleno
    public class FillInfo
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public float Transparency { get; set; }
    }

    /// Información del borde
    public class LineInfo
    {
        public float Weight { get; set; }
        public string Style { get; set; }
        public string Color { get; set; }
        public float Transparency { get; set; }
    }
    /// Información del texto
    public class TextInfo
    {
        public string Content { get; set; }
        public int WordCount { get; set; }
        public int CharacterCount { get; set; }
        public TextFormatInfo Format { get; set; }
        public List<ParagraphInfo> Paragraphs { get; set; }
    }

    /// Información del formato de texto
    public class TextFormatInfo
    {
        public string Font { get; set; }
        public float Size { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public string Color { get; set; }
    }

    /// Información de un párrafo
    public class ParagraphInfo
    {
        public string Text { get; set; }
        public string Alignment { get; set; }
        public int IndentLevel { get; set; }
    }
}