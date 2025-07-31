using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using WF = System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media ;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Size = System.Windows.Size;
using WinColors = System.Windows.Media;
using WinPoint = System.Windows;

namespace Snipper
{
    public partial class MainWindow : Window
    {
        private BitmapSource? _currentScreenshot;
        private Point _dragStartPos;
        public string PropWatermarkText { get; set; } = "Screenshot by Snipper";
        private const double MaxTotalSpacing = 100;

        public MainWindow()
        {
            InitializeComponent();
            InitializeImageEffect();

            DataContext = this;

            WatermarkToggle.IsChecked = true;
            WatermarkText.Visibility = Visibility.Visible;
        }

        private void InitializeImageEffect()
        {
            var shadowEffect = new DropShadowEffect
            {
                Color = Colors.LightSlateGray,
                Direction = 315,
                ShadowDepth = 10,
                BlurRadius = 5,
                Opacity = 0.5
            };

            ScreenshotImage.Effect = shadowEffect;
        }
        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide main window
            this.Hide();

            // Wait a moment for window to hide
            System.Threading.Thread.Sleep(200);

            // Show overlay window
            OverlayWindow overlay = new OverlayWindow();
            overlay.ScreenshotCaptured += OnScreenshotCaptured;
            overlay.ShowDialog();

            // Show main window again
            this.Show();
        }

        private void OnScreenshotCaptured(object sender, BitmapSource screenshot)
        {
            _currentScreenshot = screenshot;         // Full-res raw
            ScreenshotImage.Source = screenshot;     // Show raw image in your Viewbox

            PlaceholderText.Visibility = Visibility.Collapsed;
            PlaceHolderBorder.Visibility = Visibility.Collapsed;
            CopyButton.IsEnabled = true;
            SaveButton.IsEnabled = true;
            UpdateWatermarkSize();
        }

        private void UpdateWatermarkSize()
        {
            if (ScreenshotPreview.ActualWidth > 0)
            {
                WatermarkText.FontSize = ScreenshotPreview.ActualWidth * 0.015;         //1.5% of the actual size
            }
        }

        private void BackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string backgroundType = button?.Tag?.ToString() ?? "";

            switch (backgroundType)
            {
                case "BlueGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(220, 235, 255),
                        WinColors.Color.FromRgb(175, 205, 240),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "PurpleGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(235, 225, 245),
                        WinColors.Color.FromRgb(195, 170, 225),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "OrangeGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(255, 240, 220),
                        WinColors.Color.FromRgb(255, 205, 160),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "GrayGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(240, 240, 240),
                        WinColors.Color.FromRgb(200, 200, 200),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "BlackGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(210, 210, 210),
                        WinColors.Color.FromRgb(140, 140, 140),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "TealGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(225, 245, 240),
                        WinColors.Color.FromRgb(185, 225, 215),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "PinkGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(255, 240, 245),
                        WinColors.Color.FromRgb(250, 195, 210),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "CyanGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(230, 250, 250),
                        WinColors.Color.FromRgb(190, 230, 230),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "YellowGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(255, 255, 230),
                        WinColors.Color.FromRgb(255, 240, 180),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "RedGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(255, 235, 235),
                        WinColors.Color.FromRgb(245, 180, 180),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "IndigoGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(230, 235, 250),
                        WinColors.Color.FromRgb(185, 195, 225),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "GreenGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(235, 250, 235),
                        WinColors.Color.FromRgb(185, 225, 185),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "PeachGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(255, 245, 240),
                        WinColors.Color.FromRgb(255, 210, 190),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "SkyBlueGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(235, 245, 255),
                        WinColors.Color.FromRgb(185, 210, 235),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "EmeraldGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(235, 255, 245),
                        WinColors.Color.FromRgb(185, 225, 200),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "LavenderGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(245, 240, 255),
                        WinColors.Color.FromRgb(210, 190, 230),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "CharcoalGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(240, 240, 240),
                        WinColors.Color.FromRgb(160, 160, 160),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "LightBlueGradient":
                    BackgroundBorder.Background = new WinColors.LinearGradientBrush(
                        WinColors.Color.FromRgb(235, 245, 255),
                        WinColors.Color.FromRgb(185, 215, 240),
                        new WinPoint.Point(0.5, 0),
                        new WinPoint.Point(0.5, 1));
                    break;

                case "Transparent":
                    BackgroundBorder.Background = new DrawingBrush
                    {
                        TileMode = TileMode.Tile,
                        Viewport = new Rect(0, 0, 20, 20),
                        ViewportUnits = BrushMappingMode.Absolute,
                        Drawing = new DrawingGroup
                        {
                            Children =
                            {
                                new GeometryDrawing(Brushes.White, null,
                                    new RectangleGeometry(new Rect(0, 0, 20, 20))),
                                new GeometryDrawing(
                                    new SolidColorBrush(WinColors.Color.FromRgb(240, 240, 240)),
                                    null,
                                    new GeometryGroup
                                    {
                                        Children =
                                        {
                                            new RectangleGeometry(new Rect(0, 0, 10, 10)),
                                            new RectangleGeometry(new Rect(10, 10, 10, 10))
                                        }
                                    })
                            }
                        }
                    };
                break;
            }
        }

        private BitmapSource GetFinalImage()
        {
            return GetScreenshot(); // already high-res and decorated
        }

        private RenderTargetBitmap GetScreenshot()
        {
            double scale = 2.0; // 2x resolution

            int width = (int)(ScreenshotContainer.ActualWidth * scale);
            int height = (int)(ScreenshotContainer.ActualHeight * scale);

            var rtb = new RenderTargetBitmap(
                width,
                height,
                96 * scale,   // higher DPI
                96 * scale,
                PixelFormats.Pbgra32);

            var dv = new DrawingVisual();
            using (var ctx = dv.RenderOpen())
            {
                ctx.DrawRectangle(
                    new VisualBrush(ScreenshotContainer),
                    null,
                    new Rect(
                        new Point(),
                        new Size(ScreenshotContainer.ActualWidth, ScreenshotContainer.ActualHeight)
                    ));
            }

            rtb.Render(dv);
            rtb.Freeze();
            return rtb;
        }

        private async void ShowTempStatus(string msg, string icon = "✔")
        {
            TempSnackbarIcon.Text = icon;
            TempSnackbarText.Text = msg;

            // Slide up + fade in
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
            var slideUp = new DoubleAnimation(20, 0, TimeSpan.FromMilliseconds(200));
            TempSnackbar.BeginAnimation(OpacityProperty, fadeIn);
            ((TranslateTransform)TempSnackbar.RenderTransform).BeginAnimation(TranslateTransform.YProperty, slideUp);

            await Task.Delay(3000); // Show for 3 sec

            // Slide down + fade out
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            var slideDown = new DoubleAnimation(0, 20, TimeSpan.FromMilliseconds(200));
            TempSnackbar.BeginAnimation(OpacityProperty, fadeOut);
            ((TranslateTransform)TempSnackbar.RenderTransform).BeginAnimation(TranslateTransform.YProperty, slideDown);
        }

        private void ScreenshotImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPos = e.GetPosition(null);
        }
        private string SaveTempFile(MemoryStream imageStream)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), $"Snipper_{Guid.NewGuid()}.png");
            using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                imageStream.CopyTo(fileStream);
            }
            return tempPath;
        }

        private void ScreenshotImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _currentScreenshot != null)
            {
                var currentPos = e.GetPosition(null);

                if (Math.Abs(currentPos.X - _dragStartPos.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(currentPos.Y - _dragStartPos.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var finalImage = GetFinalImage();

                    var pngStream = new MemoryStream();
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(finalImage));
                    encoder.Save(pngStream);
                    pngStream.Position = 0;

                    var tempFile = SaveTempFile(pngStream);

                    var dataObj = new DataObject();
                    dataObj.SetData(DataFormats.Bitmap, finalImage);
                    dataObj.SetData(DataFormats.FileDrop, new string[] { tempFile });

                    DragDrop.DoDragDrop(ScreenshotImage, dataObj, DragDropEffects.Copy);
                }
            }
        }


        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            // ✅ Use scaled image only for copying
            double scale = 2.0;
            var container = ScreenshotContainer;

            int width = (int)(container.ActualWidth * scale);
            int height = (int)(container.ActualHeight * scale);

            var rtb = new RenderTargetBitmap(
                width, height,
                96 * scale, 96 * scale,
                PixelFormats.Pbgra32);

            var dv = new DrawingVisual();
            using (var ctx = dv.RenderOpen())
            {
                ctx.DrawRectangle(
                    new VisualBrush(container),
                    null,
                    new Rect(new Point(), new Size(container.ActualWidth, container.ActualHeight)));
            }
            rtb.Render(dv);

            // ✅ Encode to BMP and convert to DIB
            byte[] bmpBytes;
            using (var ms = new MemoryStream())
            {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(ms);
                bmpBytes = ms.ToArray();
            }

            byte[] dibBytes = ConvertBmpToDib(bmpBytes);

            var dataObj = new DataObject();
            using (var dibStream = new MemoryStream(dibBytes))
            {
                dataObj.SetData(DataFormats.Dib, dibStream);
                Clipboard.SetDataObject(dataObj, true);
            }

            ShowTempStatus("📋 High-res screenshot copied!");
        }



        // Converts BMP byte[] to DIB
        private static byte[] ConvertBmpToDib(byte[] bmpBytes)
        {
            const int bmpHeaderSize = 14; // BMP header is 14 bytes
            byte[] dibBytes = new byte[bmpBytes.Length - bmpHeaderSize];
            Buffer.BlockCopy(bmpBytes, bmpHeaderSize, dibBytes, 0, dibBytes.Length);
            return dibBytes;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var finalImage = GetFinalImage();

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(finalImage));

            var saveDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png",
                FileName = $"Snipper_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png"
            };

            if (saveDialog.ShowDialog() == true)
            {
                using (var fs = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    encoder.Save(fs);
                }
                ShowTempStatus("💾 Screenshot saved successfully!");
            }
        }


        private void PaddingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScreenshotImage != null)
            {
                double padding = PaddingSlider.Value;
                ScreenshotImage.Margin = new Thickness(padding);

                UpdateWatermarkSize();
            }
        }


        private void InsetSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScreenshotContainer != null)
            {
                double inset = InsetSlider.Value;
                ScreenshotContainer.Margin = new Thickness(inset);
                UpdateWatermarkSize();
            }
        }


        private void DepthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScreenshotImage?.Effect is DropShadowEffect shadowEffect)
            {
                double depth = DepthSlider.Value;

                // Adjust shadow properties
                shadowEffect.ShadowDepth = depth;
                shadowEffect.BlurRadius = depth * 0.5; // Scale blur with depth
                shadowEffect.Opacity = Math.Min(1.0, depth / 20.0); // Opacity based on depth
            }
        }

        private void ShadowBlurSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScreenshotImage?.Effect is DropShadowEffect shadowEffect)
            {
                shadowEffect.BlurRadius = ShadowBlurSlider.Value;
            }
        }

        private void ShadowOpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScreenshotImage?.Effect is DropShadowEffect shadowEffect)
            {
                shadowEffect.Opacity = ShadowOpacitySlider.Value;
            }
        }

        private void RoundedCornersSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScreenshotImage?.Effect is DropShadowEffect shadowEffect)
            {
                double radius = e.NewValue;
                ScreenshotImageBorder.CornerRadius = new CornerRadius(radius);
            }
        }

        private void WatermarkToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (WatermarkText != null) {
                WatermarkText.Visibility = Visibility.Visible;
                WatermarkInput.IsEnabled = true;
            }
        }

        private void WatermarkToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (WatermarkText != null)
            {
                WatermarkText.Visibility = Visibility.Collapsed;
                WatermarkInput.IsEnabled = false;
            }
        }

        private void WatermarkInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            WatermarkText.Text = WatermarkInput.Text;
        }

        //private void PaddingPresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (PaddingPresetComboBox.SelectedItem is ComboBoxItem selectedItem)
        //    {
        //        if (int.TryParse(selectedItem.Tag.ToString(), out int padding))
        //        {
        //            if (padding >= 0) // Not "Custom"
        //            {
        //                ScreenshotImage.Margin = new Thickness(padding);

        //                // Update slider if you have one
        //                if (PaddingSlider != null)
        //                {
        //                    PaddingSlider.Value = padding;
        //                }
        //            }
        //            // If "Custom" is selected, do nothing - let user use slider
        //        }
        //    }
        //}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWatermarkSize();
        }

    }
}