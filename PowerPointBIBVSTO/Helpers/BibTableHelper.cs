using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using System.Drawing;

namespace PowerPointBIBVSTO.Helpers
{
    public static class BibTableHelper
    {
        private const float CM_TO_PT = 28.35f;
        private const float ANCHO_TOTAL_CM = 15f;
        private const float ALTO_TOTAL_CM = 6.2f;


        private const float SLIDE_ANCHO_CM = 33.867f;
        private const float SLIDE_ALTO_CM = 19.05f;



        public static void InsertTablaBIB(int filas, int columnas, float spacingCm, bool redondeado, TablaPosition position)
        {
            var app = Globals.ThisAddIn.Application;
            var slide = app.ActiveWindow.View.Slide;
            if (slide == null) return;

            // 1. Determinar esquina superior izquierda desde posición categórica
            (float offsetX, float offsetY) = GetOffsetFromCenter(position);
            float leftCm = (SLIDE_ANCHO_CM / 2f) + offsetX;
            float topCm = (SLIDE_ALTO_CM / 2f) + offsetY;


            // 2. Convertir a puntos
            float spacingPt = spacingCm * CM_TO_PT;
            float startX = leftCm * CM_TO_PT;
            float startY = topCm * CM_TO_PT;

            // 3. Calcular tamaño de cada celda (ajustado al área máxima permitida)
            float anchoCeldaPt = (ANCHO_TOTAL_CM * CM_TO_PT - (columnas - 1) * spacingPt) / columnas;
            float altoCeldaPt = (ALTO_TOTAL_CM * CM_TO_PT - (filas - 1) * spacingPt) / filas;

            // 4. Definir forma
            var tipoForma = redondeado
                ? Office.MsoAutoShapeType.msoShapeRoundedRectangle
                : Office.MsoAutoShapeType.msoShapeRectangle;

            // 5. Colores institucionales
            int amarillo_claro = ColorTranslator.ToOle(Color.FromArgb(246, 227, 131));
            int amarillo_oscuro = ColorTranslator.ToOle(Color.FromArgb(253, 218, 36));
            int blanco = ColorTranslator.ToOle(Color.White);
            int gris_claro = ColorTranslator.ToOle(Color.FromArgb(202, 202, 202));
            int negro = ColorTranslator.ToOle(Color.Black);

            // 6. Insertar formas
            for (int f = 0; f < filas; f++)
            {
                for (int c = 0; c < columnas; c++)
                {
                    float left = startX + c * (anchoCeldaPt + spacingPt);
                    float top = startY + f * (altoCeldaPt + spacingPt);

                    var shape = slide.Shapes.AddShape(tipoForma, left, top, anchoCeldaPt, altoCeldaPt);
                    bool encabezado = f == 0;

                    shape.Fill.ForeColor.RGB = encabezado ? amarillo_claro : blanco;
                    shape.Line.Visible = Office.MsoTriState.msoTrue;
                    shape.Line.ForeColor.RGB = encabezado ? amarillo_oscuro : gris_claro;
                    shape.Line.Weight = 1.25f;

                    var tr = shape.TextFrame.TextRange;
                    tr.Font.Name = "Calibri";
                    tr.Font.Size = 12;
                    tr.Font.Color.RGB = negro;
                    tr.ParagraphFormat.Alignment = PowerPoint.PpParagraphAlignment.ppAlignCenter;
                    shape.TextFrame.VerticalAnchor = Office.MsoVerticalAnchor.msoAnchorMiddle;

                    shape.TextFrame.MarginLeft = 5;
                    shape.TextFrame.MarginRight = 5;
                    shape.TextFrame.MarginTop = 5;
                    shape.TextFrame.MarginBottom = 5;
                }
            }
        }

        private static (float deltaX, float deltaY) GetOffsetFromCenter(TablaPosition pos)
        {
            switch (pos)
            {
                case TablaPosition.TopLeft:
                    return (-15.5f, -5.43f);
                case TablaPosition.TopCenter:
                    return (0f, -5.43f);
                case TablaPosition.TopRight:
                    return (15.5f, -5.43f);
                case TablaPosition.MiddleCenter:
                    return (0f, 0f);
                case TablaPosition.BottomLeft:
                    return (-15.5f, 5.43f);
                case TablaPosition.BottomRight:
                    return (15.5f, 5.43f);
                default:
                    return (0f, 0f);
            }
        }

    }
}
