using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class EllipseTool : Tool
    {
        private Rect GetRect(Point currentPoint)
        {
            double x = Math.Min(startPoint.X, currentPoint.X);
            double y = Math.Min(startPoint.Y, currentPoint.Y);
            double width = Math.Abs(startPoint.X - currentPoint.X);
            double height = Math.Abs(startPoint.Y - currentPoint.Y);

            return new Rect(x, y, width, height);
        }

        public override void MouseMove(MouseEventArgs e, DrawingContext mainContext, DrawingContext tempContext, IInputElement relativeTo)
        {
            if (!isDrawing) return;

            Point currentPoint = e.GetPosition(relativeTo);

            Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);
            tempContext.DrawGeometry(null, pen, new EllipseGeometry(GetRect(currentPoint)));
        }

        public override void MouseUp(MouseButtonEventArgs e, DrawingContext mainContext, IInputElement relativeTo)
        {
            if (isDrawing)
            {
                Point currentPoint = e.GetPosition(relativeTo);

                Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);
                mainContext.DrawGeometry(null, pen, new EllipseGeometry(GetRect(currentPoint)));
            }
            base.MouseUp(e, mainContext, relativeTo);
        }
    }
}
