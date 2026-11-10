using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class LineTool : Tool
    {
        public override void MouseMove(MouseEventArgs e, DrawingContext drawingContext, IInputElement relativeTo)
        {
            if (!isDrawing) return;

            Point currentPoint = e.GetPosition(relativeTo);

            Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);

            drawingContext.DrawLine(pen, startPoint, currentPoint);
        }
    }
}
