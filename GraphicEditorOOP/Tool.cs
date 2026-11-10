using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public abstract class Tool
    {
        protected Point startPoint;
        protected bool isDrawing = false;
        public Color CurrentColor { get; set; } = Colors.Black;
        public double CurrentWidth { get; set; } = 2.0;

        public virtual void MouseDown(MouseButtonEventArgs e, IInputElement relativeTo)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDrawing = true;
                startPoint = e.GetPosition(relativeTo);
            }
        }

        public abstract void MouseMove(MouseEventArgs e, DrawingContext drawingContext, IInputElement relativeTo);

        public virtual void MouseUp(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                isDrawing = false;
            }
        }
    }
}
