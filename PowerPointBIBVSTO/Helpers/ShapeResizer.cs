using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace PowerPointBIBVSTO.Helpers
{
    public static class ShapeResizer
    {
        private static float _width;
        private static float _height;

        public static void StoreSize()
        {
            var selection = Globals.ThisAddIn.Application.ActiveWindow.Selection;
            if (selection.Type == PowerPoint.PpSelectionType.ppSelectionShapes &&
                selection.ShapeRange.Count >= 1)
            {
                var shape = selection.ShapeRange[1];
                _width = shape.Width;
                _height = shape.Height;
            }
        }

        public static void ApplyStoredSize()
        {
            var selection = Globals.ThisAddIn.Application.ActiveWindow.Selection;
            if (selection.Type == PowerPoint.PpSelectionType.ppSelectionShapes &&
                selection.ShapeRange.Count >= 1)
            {
                foreach (PowerPoint.Shape shape in selection.ShapeRange)
                {
                    shape.Width = _width;
                    shape.Height = _height;
                }
            }
        }
    }
}
