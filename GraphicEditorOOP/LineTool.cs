using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class LineTool : Tool
    {
        public override void MouseMove(MouseEventArgs e, DrawingContext mainContext, DrawingContext tempContext, IInputElement relativeTo)
        {
            if (!isDrawing) return;

            Point currentPoint = e.GetPosition(relativeTo);

            Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);
            tempContext.DrawLine(pen, startPoint, currentPoint);
        }

        public override void MouseUp(MouseButtonEventArgs e, DrawingContext mainContext, IInputElement relativeTo)
        {
            if (isDrawing)
            {
                Point currentPoint = e.GetPosition(relativeTo);

                Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);
                mainContext.DrawLine(pen, startPoint, currentPoint);
            }
            base.MouseUp(e, mainContext, relativeTo);
        }
    }
}
