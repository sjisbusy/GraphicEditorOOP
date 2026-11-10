using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace GraphicEditorOOP
{
    public class DrawingCanvas : FrameworkElement
    {
        private readonly List<Visual> _visuals = new List<Visual>();
        private readonly DrawingVisual _tempVisual = new DrawingVisual();

        public DrawingCanvas()
        {
            AddVisualChild(_tempVisual);
        }

        public void AddVisual(Visual visual)
        {
            _visuals.Add(visual);
            AddVisualChild(visual);
            AddLogicalChild(visual);
            InvalidateVisual();
        }

        public DrawingContext OpenTemp() => _tempVisual.RenderOpen();

        public void Clear()
        {
            foreach (var visual in _visuals)
            {
                RemoveVisualChild(visual);
                RemoveLogicalChild(visual);
            }
            _visuals.Clear();

            using (var g = _tempVisual.RenderOpen()) { }

            InvalidateVisual();
        }

        protected override int VisualChildrenCount => _visuals.Count + 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index == _visuals.Count) return _tempVisual;
            return _visuals[index];
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            using (DrawingContext dc = OpenTemp())
            {
                dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, arrangeSize.Width, arrangeSize.Height));
            }
            return base.ArrangeOverride(arrangeSize);
        }

    }
}

