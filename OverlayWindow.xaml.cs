using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;  // For Path


namespace Snipper
{
    public partial class OverlayWindow : Window
    {
        private bool _isSelecting;
        private System.Windows.Point _startPoint;
        private System.Windows.Point _endPoint;

        private System.Windows.Shapes.Rectangle TopOverlay, BottomOverlay, LeftOverlay, RightOverlay;



        public event EventHandler<BitmapSource>? ScreenshotCaptured;

        public OverlayWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => SetupOverlay();
        }

        private void SetupOverlay()
        {
            // Set window to cover all screens
            this.Left = SystemParameters.VirtualScreenLeft;
            this.Top = SystemParameters.VirtualScreenTop;
            this.Width = SystemParameters.VirtualScreenWidth;
            this.Height = SystemParameters.VirtualScreenHeight;

            // Set canvas size to match window
            OverlayCanvas.Width = this.Width;
            OverlayCanvas.Height = this.Height;

            // Create initial full overlay
            CreateInitialOverlay();
        }
        private void CreateInitialOverlay()
        {
            var overlayBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));

            // Create a single overlay rectangle initially
            TopOverlay = new System.Windows.Shapes.Rectangle
            {
                Fill = overlayBrush,
                Width = OverlayCanvas.Width,
                Height = OverlayCanvas.Height
            };

            Canvas.SetLeft(TopOverlay, 0);
            Canvas.SetTop(TopOverlay, 0);
            OverlayCanvas.Children.Add(TopOverlay);
        }
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isSelecting = true;
            _startPoint = e.GetPosition(OverlayCanvas);

            // Show selection rectangle
            SelectionRect.Visibility = Visibility.Visible;
            Canvas.SetLeft(SelectionRect, _startPoint.X);
            Canvas.SetTop(SelectionRect, _startPoint.Y);
            SelectionRect.Width = 0;
            SelectionRect.Height = 0;

            OverlayCanvas.CaptureMouse();

            // Show coordinate display when starting selection
            CoordinateText.Visibility = Visibility.Visible;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelecting)
            {
                _endPoint = e.GetPosition(OverlayCanvas);
                double left = Math.Min(_startPoint.X, _endPoint.X);
                double top = Math.Min(_startPoint.Y, _endPoint.Y);
                double width = Math.Abs(_endPoint.X - _startPoint.X);
                double height = Math.Abs(_endPoint.Y - _startPoint.Y);

                // Update selection rectangle
                Canvas.SetLeft(SelectionRect, left);    
                Canvas.SetTop(SelectionRect, top);
                SelectionRect.Width = width;
                SelectionRect.Height = height;

                UpdateCoordinateDisplay(_endPoint);

                // Create the 4-part overlay with cutout
                if (width > 1 && height > 1)
                {
                    CreateFourPartOverlay(left, top, width, height);
                }
            }
        }
        private void UpdateCoordinateDisplay(System.Windows.Point mousePos)
        {
            CoordinateText.Text = $"X: {(int)mousePos.X}, Y: {(int)mousePos.Y}";

            // Positioning the text near the cursor (offset to avoid blocking view)
            double textLeft = mousePos.X + 15;
            double textTop = mousePos.Y - 25;

            // Keeping text within canvas bounds
            if (textLeft + 100 > OverlayCanvas.ActualWidth) // Approximate text width
                textLeft = mousePos.X - 100 - 15;

            if (textTop < 0)
                textTop = mousePos.Y + 15;

            Canvas.SetLeft(CoordinateText, textLeft);
            Canvas.SetTop(CoordinateText, textTop);

            // Show during selection
            CoordinateText.Visibility = Visibility.Visible;
        }
        private void CreateFourPartOverlay(double selectionLeft, double selectionTop, double selectionWidth, double selectionHeight)
        {
            // Remove existing overlays
            if (TopOverlay != null) OverlayCanvas.Children.Remove(TopOverlay);
            if (BottomOverlay != null) OverlayCanvas.Children.Remove(BottomOverlay);
            if (LeftOverlay != null) OverlayCanvas.Children.Remove(LeftOverlay);
            if (RightOverlay != null) OverlayCanvas.Children.Remove(RightOverlay);

            var overlayBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 0, 0, 0));
            double canvasWidth = OverlayCanvas.Width;
            double canvasHeight = OverlayCanvas.Height;

            // Top rectangle
            TopOverlay = new System.Windows.Shapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(TopOverlay, 0);
            Canvas.SetTop(TopOverlay, 0);
            TopOverlay.Width = canvasWidth;
            TopOverlay.Height = selectionTop;
            OverlayCanvas.Children.Add(TopOverlay);

            // Bottom rectangle
            BottomOverlay = new System.Windows.Shapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(BottomOverlay, 0);
            Canvas.SetTop(BottomOverlay, selectionTop + selectionHeight);
            BottomOverlay.Width = canvasWidth;
            BottomOverlay.Height = canvasHeight - (selectionTop + selectionHeight);
            OverlayCanvas.Children.Add(BottomOverlay);

            // Left rectangle
            LeftOverlay = new System.Windows.Shapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(LeftOverlay, 0);
            Canvas.SetTop(LeftOverlay, selectionTop);
            LeftOverlay.Width = selectionLeft;
            LeftOverlay.Height = selectionHeight;
            OverlayCanvas.Children.Add(LeftOverlay);

            // Right rectangle
            RightOverlay = new System.Windows.Shapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(RightOverlay, selectionLeft + selectionWidth);
            Canvas.SetTop(RightOverlay, selectionTop);
            RightOverlay.Width = canvasWidth - (selectionLeft + selectionWidth);
            RightOverlay.Height = selectionHeight;
            OverlayCanvas.Children.Add(RightOverlay);
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isSelecting)
            {
                _isSelecting = false;
                OverlayCanvas.ReleaseMouseCapture();

                // Calculate selection area
                double left = Math.Min(_startPoint.X, _endPoint.X);
                double top = Math.Min(_startPoint.Y, _endPoint.Y);
                double width = Math.Abs(_endPoint.X - _startPoint.X);
                double height = Math.Abs(_endPoint.Y - _startPoint.Y);

                if (width > 10 && height > 10) // Minimum size check
                {
                    // Convert to screen coordinates
                    System.Windows.Point screenPoint = this.PointToScreen(new System.Windows.Point(left, top));

                    // Capture screenshot
                    BitmapSource screenshot = CaptureScreen(
                        (int)screenPoint.X, (int)screenPoint.Y,
                        (int)width, (int)height);

                    ScreenshotCaptured?.Invoke(this, screenshot);
                }

                this.Close();
            }
            // Hide coordinate display when selection is complete
            CoordinateText.Visibility = Visibility.Collapsed;
        }

        private BitmapSource CaptureScreen(int x, int y, int width, int height)
        {
            using (Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height),
                        CopyPixelOperation.SourceCopy);
                }

                return ConvertBitmapToBitmapSource(bitmap);
            }
        }

        private BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgra32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            base.OnKeyDown(e);
        }
    }
}