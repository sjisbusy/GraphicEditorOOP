using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class PencilTool : Tool
    {
        private PathGeometry? currentGeometry;
        private PathFigure? currentFigure;

        public override void MouseDown(MouseButtonEventArgs e, IInputElement relativeTo)
        {
            base.MouseDown(e, relativeTo);

            if (isDrawing)
            {
                currentGeometry = new PathGeometry();
                currentFigure = new PathFigure { StartPoint = startPoint };
                currentGeometry.Figures.Add(currentFigure);
            }
        }

        public override void MouseMove(MouseEventArgs e, DrawingContext drawingContext, IInputElement relativeTo)
        {
            if (!isDrawing || currentGeometry == null || currentFigure == null) return;

            Point currentPoint = e.GetPosition(relativeTo);

            LineSegment segment = new LineSegment(currentPoint, true);
            currentFigure.Segments.Add(segment);

            Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);

            drawingContext.DrawGeometry(null, pen, currentGeometry);
        }
    }
}
