using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class EllipseTool : Tool
    {
        public override void MouseMove(MouseEventArgs e, DrawingContext drawingContext, IInputElement relativeTo)
        {
            if (!isDrawing) return;

            Point currentPoint = e.GetPosition(relativeTo);
            double x = Math.Min(startPoint.X, currentPoint.X);
            double y = Math.Min(startPoint.Y, currentPoint.Y);
            double width = Math.Abs(startPoint.X - currentPoint.X);
            double height = Math.Abs(startPoint.Y - currentPoint.Y);

            if (width <= 0 || height <= 0) return;

            Pen pen = new Pen(new SolidColorBrush(CurrentColor), CurrentWidth);

            Rect boundingBox = new Rect(x, y, width, height);

            EllipseGeometry ellipseGeometry = new EllipseGeometry(boundingBox);

            drawingContext.DrawGeometry(null, pen, ellipseGeometry);
        }
    }
}
