using System.Windows;
using System.Windows.Media;

namespace GraphicEditorOOP
{
    public class DrawingCanvas : FrameworkElement
    {
        private readonly DrawingVisual _mainVisual = new DrawingVisual();
        private readonly DrawingVisual _tempVisual = new DrawingVisual();

        public DrawingCanvas()
        {
            AddVisualChild(_mainVisual);
            AddVisualChild(_tempVisual);
        }

        public DrawingContext OpenMain() => _mainVisual.RenderOpen();
        public DrawingContext OpenTemp() => _tempVisual.RenderOpen();

        public void Clear()
        {
            using (var g = _mainVisual.RenderOpen()) { }
            using (var g = _tempVisual.RenderOpen()) { }
        }

        protected override int VisualChildrenCount => 2;

        protected override Visual GetVisualChild(int index)
        {
            return index == 0 ? _mainVisual : _tempVisual;
        }
    }
}
