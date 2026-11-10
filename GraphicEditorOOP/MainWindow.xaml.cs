using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace GraphicEditorOOP
{
    public partial class MainWindow : Window
    {
        private Tool _activeTool;
        private readonly DrawingCanvas _canvas;
        private DrawingVisual _currentVisual;

        public MainWindow()
        {
            InitializeComponent();

            _canvas = new DrawingCanvas();
            _canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
            _canvas.VerticalAlignment = VerticalAlignment.Stretch;

            CanvasContainer.Children.Add(_canvas);

            _canvas.MouseDown += Canvas_MouseDown;
            _canvas.MouseMove += Canvas_MouseMove;
            _canvas.MouseUp += Canvas_MouseUp;

            _activeTool = new PencilTool();
            UpdateToolSettings();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _activeTool.MouseDown(e, _canvas);

            _currentVisual = new DrawingVisual();

            _canvas.CaptureMouse();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!e.LeftButton.Equals(MouseButtonState.Pressed)) return;

            if (_activeTool is PencilTool)
            {
                DrawingVisual strokeVisual = new DrawingVisual();
                using (DrawingContext mainContext = strokeVisual.RenderOpen())
                {
                    _activeTool.MouseMove(e, mainContext, null, _canvas);
                }
                _canvas.AddVisual(strokeVisual);
            }
            else
            {
                if (_currentVisual == null) return;

                using (DrawingContext mainContext = _currentVisual.RenderOpen())
                using (DrawingContext tempContext = _canvas.OpenTemp())
                {
                    _activeTool.MouseMove(e, mainContext, tempContext, _canvas);
                }
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_currentVisual != null)
            {
                using (DrawingContext mainContext = _currentVisual.RenderOpen())
                using (DrawingContext tempContext = _canvas.OpenTemp())
                {
                    _activeTool.MouseUp(e, mainContext, _canvas);
                }

                _canvas.AddVisual(_currentVisual);
                _currentVisual = null;
            }

            using (DrawingContext tempContext = _canvas.OpenTemp()) { }

            _canvas.ReleaseMouseCapture();
        }

        private void Tool_Checked(object sender, RoutedEventArgs e)
        {
            if (_canvas == null) return;

            RadioButton rb = (RadioButton)sender;

            if (rb.Name == "BtnPencil") _activeTool = new PencilTool();
            else if (rb.Name == "BtnLine") _activeTool = new LineTool();
            else if (rb.Name == "BtnRectangle") _activeTool = new RectangleTool();
            else if (rb.Name == "BtnEllipse") _activeTool = new EllipseTool();

            UpdateToolSettings();
        }

        private void UpdateToolSettings()
        {
            if (_activeTool == null || ComboColor == null || SliderWidth == null) return;

            ComboBoxItem selectedColorItem = (ComboBoxItem)ComboColor.SelectedItem;
            string colorTag = selectedColorItem.Tag.ToString();

            _activeTool.CurrentColor = (Color)ColorConverter.ConvertFromString(colorTag);
            _activeTool.CurrentWidth = SliderWidth.Value;
        }

        private void ComboColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateToolSettings();
        }

        private void SliderWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateToolSettings();
            if (TxtWidthValue != null)
            {
                TxtWidthValue.Text = $"{(int)SliderWidth.Value} px";
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            _canvas.Clear();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg",
                Title = "Сохранить готовое изображение",
                FileName = "Рисунок"
            };

            if (saveDialog.ShowDialog() == true)
            {
                int width = (int)_canvas.ActualWidth;
                int height = (int)_canvas.ActualHeight;

                if (width <= 0 || height <= 0) return;

                RenderTargetBitmap renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
                renderTarget.Render(_canvas);

                BitmapEncoder encoder = saveDialog.FilterIndex == 1 ? new PngBitmapEncoder() : new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));

                using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    encoder.Save(stream);
                }

                MessageBox.Show("Изображение успешно сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
