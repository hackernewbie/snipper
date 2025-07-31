using System;
using System.Media;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using DrawingImaging = System.Drawing.Imaging;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Drawing.Drawing2D;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using Brushes = System.Drawing.Brushes;
using WinShapes = System.Windows.Shapes;
using System.Diagnostics;  // For Path


namespace Snipper
{
    public partial class OverlayWindow : Window
    {
        private bool _isSelecting;
        private System.Windows.Point _startPoint;
        private System.Windows.Point _endPoint;
        private WinShapes.Rectangle TopOverlay, BottomOverlay, LeftOverlay, RightOverlay;

        public event EventHandler<BitmapSource>? ScreenshotCaptured;

        public OverlayWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => SetupOverlay();
        }

        private void SetupOverlay()
        {
            this.Cursor = new Cursor("resources/crosshair.cur");

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
            TopOverlay = new WinShapes.Rectangle
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
            TopOverlay = new WinShapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(TopOverlay, 0);
            Canvas.SetTop(TopOverlay, 0);
            TopOverlay.Width = canvasWidth;
            TopOverlay.Height = selectionTop;
            OverlayCanvas.Children.Add(TopOverlay);

            // Bottom rectangle
            BottomOverlay = new WinShapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(BottomOverlay, 0);
            Canvas.SetTop(BottomOverlay, selectionTop + selectionHeight);
            BottomOverlay.Width = canvasWidth;
            BottomOverlay.Height = canvasHeight - (selectionTop + selectionHeight);
            OverlayCanvas.Children.Add(BottomOverlay);

            // Left rectangle
            LeftOverlay = new WinShapes.Rectangle { Fill = overlayBrush };
            Canvas.SetLeft(LeftOverlay, 0);
            Canvas.SetTop(LeftOverlay, selectionTop);
            LeftOverlay.Width = selectionLeft;
            LeftOverlay.Height = selectionHeight;
            OverlayCanvas.Children.Add(LeftOverlay);

            // Right rectangle
            RightOverlay = new WinShapes.Rectangle { Fill = overlayBrush };
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

                double left = Math.Min(_startPoint.X, _endPoint.X);
                double top = Math.Min(_startPoint.Y, _endPoint.Y);
                double width = Math.Abs(_endPoint.X - _startPoint.X);
                double height = Math.Abs(_endPoint.Y - _startPoint.Y);

                if (width > 10 && height > 10)
                {
                    // Convert to screen coordinates
                    System.Windows.Point screenPoint = this.PointToScreen(new System.Windows.Point(left, top));

                    // Detect current DPI
                    var dpi = VisualTreeHelper.GetDpi(this);

                    // Scale width/height for physical pixels
                    int captureLeft = (int)(screenPoint.X);
                    int captureTop = (int)(screenPoint.Y);
                    int captureWidth = (int)(width * dpi.DpiScaleX);
                    int captureHeight = (int)(height * dpi.DpiScaleY);

                    BitmapSource screenshot = ScreenCaptureHelper.CaptureScreen(
                        captureLeft, captureTop, captureWidth, captureHeight
                    );

                    ScreenshotCaptured?.Invoke(this, screenshot);
                    PlayScreenshotSound();
                }

                this.Close();
            }
            CoordinateText.Visibility = Visibility.Collapsed;
        }


        public static class ScreenCaptureHelper
        {
            [DllImport("user32.dll")]
            private static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            private static extern IntPtr GetWindowDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

            [DllImport("gdi32.dll")]
            private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

            [DllImport("gdi32.dll")]
            private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
                                              IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

            [DllImport("gdi32.dll")]
            private static extern bool DeleteDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            private static extern bool DeleteObject(IntPtr hObject);

            private const int SRCCOPY = 0x00CC0020;

            public static BitmapSource CaptureScreen(int x, int y, int width, int height)
            {
                IntPtr desktopWnd = GetDesktopWindow();
                IntPtr desktopDC = GetWindowDC(desktopWnd);
                IntPtr memoryDC = CreateCompatibleDC(desktopDC);

                IntPtr hBitmap = CreateCompatibleBitmap(desktopDC, width, height);
                IntPtr oldBitmap = SelectObject(memoryDC, hBitmap);

                BitBlt(memoryDC, 0, 0, width, height, desktopDC, x, y, SRCCOPY);

                try
                {
                    var bmpSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap, IntPtr.Zero, Int32Rect.Empty,
                        BitmapSizeOptions.FromWidthAndHeight(width, height));

                    return bmpSource;
                }
                finally
                {
                    SelectObject(memoryDC, oldBitmap);
                    DeleteObject(hBitmap);
                    DeleteDC(memoryDC);
                    ReleaseDC(desktopWnd, desktopDC);
                }
            }
        }
        
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void PlayScreenshotSound()
        {
            try
            {
                var player = new SoundPlayer("resources/click.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                // Optional: log or silently ignore
            }
        }

        private RenderTargetBitmap AddWatermark(System.Windows.Media.Imaging.BitmapSource source, string watermarkText)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;

            // Create a visual to draw both the image and watermark
            var visual = new System.Windows.Media.DrawingVisual();
            using (var dc = visual.RenderOpen())
            {
                // Draw original image
                dc.DrawImage(source, new System.Windows.Rect(0, 0, width, height));

                // Create dynamic TextBlock for watermark
                var textBlock = new System.Windows.Controls.TextBlock
                {
                    Text = watermarkText,
                    Foreground = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromArgb(150, 255, 255, 255)), // semi-transparent white
                    FontSize = width * 0.015, // 1.5% of image width
                    FontFamily = new System.Windows.Media.FontFamily("Segoe UI")
                };

                // Measure & arrange text
                textBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                textBlock.Arrange(new System.Windows.Rect(textBlock.DesiredSize));

                // Calculate bottom-right position
                double x = width - textBlock.DesiredSize.Width - (width * 0.01);
                double y = height - textBlock.DesiredSize.Height - (height * 0.01);

                // Draw the watermark
                dc.PushOpacity(0.6);
                dc.DrawRectangle(
                    new System.Windows.Media.VisualBrush(textBlock),
                    null,
                    new System.Windows.Rect(x, y, textBlock.DesiredSize.Width, textBlock.DesiredSize.Height));
                dc.Pop();
            }

            // Render the visual to a new bitmap
            var renderTarget = new System.Windows.Media.Imaging.RenderTargetBitmap(
                width, height, 96, 96, System.Windows.Media.PixelFormats.Pbgra32);
            renderTarget.Render(visual);

            return renderTarget;
        }

    }
}