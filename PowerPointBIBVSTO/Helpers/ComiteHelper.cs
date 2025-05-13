using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System;

namespace PowerPointBIBVSTO.Helpers
{
    public static class ComiteHelper
    {
        public static void CrearComite(string comiteNombre)
        {
            var app = Globals.ThisAddIn.Application;
            PowerPoint.Presentation pres = (app.Presentations.Count == 0)
                ? app.Presentations.Add(Office.MsoTriState.msoTrue)
                : app.ActivePresentation;

            /* ── 1. Insertar layouts al final ────────────────────────────── */
            var orden = new (string layout, int rep)[]
            {
                ("Portada",            1),
                ("INTRODUCCION",       1),
                ("Tabla_Contenido",    1),
                ("Comienzo_Seccion",   4),
                ("TERM_SHEET",         2),
                ("Final",              1)
            };

            foreach (var (layout, rep) in orden)
            {
                int idx = BuscarLayout(pres, layout);
                if (idx == 0)
                {
                    MessageBox.Show($"No se encontró el layout «{layout}».",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                for (int i = 0; i < rep; i++)
                    pres.Slides.AddSlide(pres.Slides.Count + 1,
                                         pres.SlideMaster.CustomLayouts[idx]);
            }

            /* ── 2. Crear secciones seguras ──────────────────────────────── */
            var sections = pres.SectionProperties;
            try { if (sections.Count > 0) sections.Delete(1, false); } catch { }

            // Helper local para añadir solo si no existe ya
            void AddSectionBeforeSlide(int slideIdx, string name)
            {
                if (slideIdx < 1 || slideIdx > pres.Slides.Count) return;
                sections.AddBeforeSlide(slideIdx, name);
            }

            // A. Portada / Introducción / Tabla de Contenido
            for (int i = 1; i <= pres.Slides.Count; i++)
            {
                string ln = pres.Slides[i].CustomLayout.Name.ToUpperInvariant();
                if (ln == "PORTADA") { AddSectionBeforeSlide(i, "Portada"); break; }
            }
            for (int i = 1; i <= pres.Slides.Count; i++)
            {
                string ln = pres.Slides[i].CustomLayout.Name.ToUpperInvariant();
                if (ln == "INTRODUCCION") { AddSectionBeforeSlide(i, "Introducción"); break; }
            }
            for (int i = 1; i <= pres.Slides.Count; i++)
            {
                string ln = pres.Slides[i].CustomLayout.Name.ToUpperInvariant();
                if (ln == "TABLA_CONTENIDO") { AddSectionBeforeSlide(i, "Tabla de Contenido"); break; }
            }

            // B. Secciones dinámicas a partir de cada COMIENZO_SECCION
            int secCounter = 1;
            for (int i = 1; i <= pres.Slides.Count; i++)
            {
                if (pres.Slides[i].CustomLayout.Name.ToUpperInvariant() == "COMIENZO_SECCION")
                {
                    AddSectionBeforeSlide(i, $"Sección {secCounter++}");
                }
            }

            // (Opcional) llevar al usuario a la Portada recién creada
            pres.Windows[1].Activate();
            pres.Slides[GetSlideIndexByLayout(pres, "PORTADA")]?.Select();
        }

        /* ── Helpers ─────────────────────────────────────────────────────── */
        private static int BuscarLayout(PowerPoint.Presentation pres, string nombre)
        {
            var layouts = pres.SlideMaster.CustomLayouts;
            for (int i = 1; i <= layouts.Count; i++)
                if (string.Equals(layouts[i].Name, nombre, System.StringComparison.OrdinalIgnoreCase))
                    return i;
            return 0;   // no encontrado
        }

        private static int GetSlideIndexByLayout(PowerPoint.Presentation pres, string nombre)
        {
            for (int i = 1; i <= pres.Slides.Count; i++)
                if (pres.Slides[i].CustomLayout.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                    return i;
            return 1;
        }
    }
}
