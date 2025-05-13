using System;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using PowerPointBIBVSTO.Helpers;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PowerPointBIBVSTO
{
    public partial class RibbonBIB
    {
        private float _espaciadoPt = 8f;                  // distancia en puntos
        private const float MAX_CM = 1.0f;                // máximo 1 cm
        private const float CM_TO_PT = 28.35f;            // conversión
        private TablaPosition _posicionSeleccionada = TablaPosition.TopLeft;


        private void RibbonBIB_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void btnNE_Click(object sender, RibbonControlEventArgs e)
        {
            ComiteHelper.CrearComite("Negocios Estructurados");
        }

        private void btnCred_Click(object sender, RibbonControlEventArgs e)
        {
            ComiteHelper.CrearComite("Crédito");
        }

        private void btnApetito_Click(object sender, RibbonControlEventArgs e)
        {
            ComiteHelper.CrearComite("Apetito");
        }

        private void btnBiblioteca_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.MostrarSlidePicker();
        }

        private void btnWebPanel_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ToggleReactPane(); // o cualquier otra URL del panel Office.js
        }

        private void btnGetSize_Click(object sender, RibbonControlEventArgs e)
        {
            var selection = Globals.ThisAddIn.Application.ActiveWindow.Selection;

            if (selection.Type == PowerPoint.PpSelectionType.ppSelectionShapes &&
                selection.ShapeRange.Count > 0)
            {
                var shape = selection.ShapeRange[1];
                ShapeSizeHelper.Width = shape.Width;
                ShapeSizeHelper.Height = shape.Height;
        
            }
        }

        private void btnSetSize_Click(object sender, RibbonControlEventArgs e)
        {
            var selection = Globals.ThisAddIn.Application.ActiveWindow.Selection;

            if (selection.Type == PowerPoint.PpSelectionType.ppSelectionShapes &&
                selection.ShapeRange.Count > 0)
            {
                foreach (PowerPoint.Shape shape in selection.ShapeRange)
                {
                    shape.Width = ShapeSizeHelper.Width;
                    shape.Height = ShapeSizeHelper.Height;
                }
            }
            
        }


        private void btnActualizarToC_Click(object sender, RibbonControlEventArgs e)
        {
            TableOfContentsUpdater.UpdateTableOfContents();
        }

        private void IAButton_Click(object sender, RibbonControlEventArgs e)
        {
            var panel = new ucChatGPTPanel();
            var ctp = Globals.ThisAddIn.CustomTaskPanes.Add(panel, "Asistente IA");
            ctp.Width = 550;
            ctp.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
            ctp.Visible = true;
        }

        private void btnInsertarRespuesta(object sender, RibbonControlEventArgs e)
        {
            var respuesta = ucChatGPTPanel.UltimaRespuesta;

            if (string.IsNullOrWhiteSpace(respuesta))
            {
                MessageBox.Show("No hay ninguna respuesta generada aún.", "Sin respuesta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var app = Globals.ThisAddIn.Application;
            var selection = app.ActiveWindow.Selection;

            if (selection.Type != PowerPoint.PpSelectionType.ppSelectionShapes || selection.ShapeRange.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una forma en la diapositiva para insertar el texto.", "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (PowerPoint.Shape shape in selection.ShapeRange)
            {
                if (shape.HasTextFrame == Office.MsoTriState.msoTrue &&
                    shape.TextFrame.HasText == Office.MsoTriState.msoTrue)
                {
                    shape.TextFrame.TextRange.Text = respuesta;
                }
            }
        }

        private void btn3x3_Click(object sender, RibbonControlEventArgs e)
        {
            BibTableHelper.InsertTablaBIB(3, 3, spacingCm: GetEspaciadoCm(), redondeado: checkRedondos.Checked, position: _posicionSeleccionada);
        }
        private void btn4x4_Click(object sender, RibbonControlEventArgs e)
        {
            BibTableHelper.InsertTablaBIB(4, 4, spacingCm: GetEspaciadoCm(), redondeado: checkRedondos.Checked, position: _posicionSeleccionada);
        }
        private void btn5x5_Click(object sender, RibbonControlEventArgs e)
        {
            BibTableHelper.InsertTablaBIB(5, 5, spacingCm: GetEspaciadoCm(), redondeado: checkRedondos.Checked, position: _posicionSeleccionada);
        }
        private void btn6x6_Click(object sender, RibbonControlEventArgs e)
        {
            BibTableHelper.InsertTablaBIB(6, 6, spacingCm: GetEspaciadoCm(), redondeado: checkRedondos.Checked, position: _posicionSeleccionada);
        }

        private void checkRedondos_Click(object sender, RibbonControlEventArgs e)
        {

        }
        private void comboDistancia_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void toggleButton1_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void editBox1_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void drpPosition_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            var seleccion = drpPosition.SelectedItem?.Tag.ToString();
            if (Enum.TryParse<TablaPosition>(seleccion, out var pos))
            {
                _posicionSeleccionada = pos;
            }

        }
        private float GetEspaciadoCm()
        {
            if (float.TryParse(editDistancia.Text.Replace(",", "."), System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out float cm))
            {
                if (cm < 0) cm = 0;
                if (cm > 1.0f) cm = 1.0f;
                return cm;
            }

            return 0.3f; // valor por defecto si el input es inválido
        }


        private void editDistancia_TextChanged(object sender, RibbonControlEventArgs e)
        {

        }
    }
}
