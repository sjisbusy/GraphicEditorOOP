using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class PencilTool : Tool
    {
        public override void MouseMove(MouseEventArgs e, DrawingContext mainContext, DrawingContext tempContext, IInputElement relativeTo)
        {
            if (!isDrawing) return;

            Point currentPoint = e.GetPosition(relativeTo);

            if (startPoint == currentPoint) return;

            Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);

            pen.StartLineCap = PenLineCap.Round;
            pen.LineJoin = PenLineJoin.Round;
            pen.EndLineCap = PenLineCap.Round;

            mainContext.DrawLine(pen, startPoint, currentPoint);

            startPoint = currentPoint;
        }
    }
}
