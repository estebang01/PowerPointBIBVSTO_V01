using System;
using System.Collections.Generic;
using System.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace PowerPointBIBVSTO.Helpers
{
    public static class TableOfContentsUpdater
    {
        public static void UpdateTableOfContents()
        {
            var app = Globals.ThisAddIn.Application;
            var presentation = app.ActivePresentation;
            string layoutTargetName = "TABLA_CONTENIDO";
            PowerPoint.Slide tocSlide = null;
            string resultText = string.Empty;
            var sectionNames = new List<string>();

            // Paso 1: Encontrar la slide con el layout objetivo
            foreach (PowerPoint.Slide slide in presentation.Slides)
            {
                try
                {
                    if (slide.CustomLayout.Name == layoutTargetName)
                    {
                        tocSlide = slide;
                        break;
                    }
                }
                catch { }
            }

            if (tocSlide == null)
            {
                System.Windows.Forms.MessageBox.Show(
                    "No se encontró una diapositiva con el layout 'TABLA_CONTENIDO'.",
                    "Error", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            // Paso 2: Obtener los nombres de las secciones
            int lastSectionIndex = -1;
            for (int i = 1; i <= presentation.Slides.Count; i++)
            {
                var slide = presentation.Slides[i];
                try
                {
                    int sectionIndex = slide.sectionIndex;
                    if (sectionIndex > 0 && sectionIndex != lastSectionIndex)
                    {
                        string sectionName = presentation.SectionProperties.Name(sectionIndex);
                        sectionNames.Add(sectionName);
                        lastSectionIndex = sectionIndex;
                    }
                }
                catch { }
            }

            resultText = string.Join(Environment.NewLine, sectionNames);

            // Paso 4: Insertar el texto en el primer placeholder de tipo Body
            foreach (PowerPoint.Shape shape in tocSlide.Shapes)
            {
                if (shape.Type == Office.MsoShapeType.msoPlaceholder &&
                    shape.PlaceholderFormat.Type == PowerPoint.PpPlaceholderType.ppPlaceholderBody)
                {
                    shape.TextFrame.TextRange.Text = resultText;
                    break;
                }
            }

            System.Windows.Forms.MessageBox.Show("Tabla de contenido actualizada correctamente.",
                "Éxito", System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
