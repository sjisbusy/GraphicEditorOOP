using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class RectangleTool : Tool
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

            Rect rect = new Rect(x, y, width, height);

            drawingContext.DrawRectangle(null, pen, rect);
        }

    }
}
