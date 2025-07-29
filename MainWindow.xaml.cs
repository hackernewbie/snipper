using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media ;
using WinPoint = System.Windows;
using WinColors = System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Size = System.Windows.Size;
using Brushes = System.Windows.Media.Brushes;

namespace Snipper
{
    public partial class MainWindow : Window
    {
        private BitmapSource? _currentScreenshot;
        public string PropWatermarkText { get; set; } = "Screenshot by Snipper";

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
            _currentScreenshot = screenshot;        // full-quality
            ScreenshotImage.Source = screenshot;    // no extra scaling

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


        private RenderTargetBitmap CaptureAtHighRes(FrameworkElement target)
        {
            var originalTransform = target.LayoutTransform;

            try
            {
                var dpi = VisualTreeHelper.GetDpi(target);

                // Capture at actual physical pixel size
                double actualWidth = target.ActualWidth * dpi.DpiScaleX;
                double actualHeight = target.ActualHeight * dpi.DpiScaleY;

                target.LayoutTransform = Transform.Identity;
                target.Measure(new Size(target.ActualWidth, target.ActualHeight));
                target.Arrange(new Rect(0, 0, target.ActualWidth, target.ActualHeight));
                target.UpdateLayout();

                var rtb = new RenderTargetBitmap(
                    (int)actualWidth,
                    (int)actualHeight,
                    dpi.PixelsPerInchX,
                    dpi.PixelsPerInchY,
                    PixelFormats.Pbgra32);

                rtb.Render(target);
                return rtb;
            }
            finally
            {
                target.LayoutTransform = originalTransform;
                target.UpdateLayout();
            }
        }

        private RenderTargetBitmap GetScreenshot()
        {
            return CaptureAtHighRes(ScreenshotContainer);
        }


        /// <summary>Saves the given bitmap using a SaveFileDialog.</summary>
        private void SaveBitmapWithDialog(RenderTargetBitmap bmp)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg",
                DefaultExt = "png",
                FileName = $"Snipper_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}"
            };

            if (dialog.ShowDialog() != true)
                return;

            BitmapEncoder encoder = dialog.FilterIndex == 1
                ? new PngBitmapEncoder()
                : new JpegBitmapEncoder { QualityLevel = 100 };

            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (var fileSystem = File.Create(dialog.FileName))
                encoder.Save(fileSystem);

            ShowTempStatus($"Screenshot saved to {dialog.FileName}");
        }

        
        private async void ShowTempStatus(string msg)
        {
            string old = Title;
            Title = msg;
            PlaceholderText.Text = msg;
            await Task.Delay(3000);
            Title = old;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentScreenshot == null) return;

            Clipboard.SetImage(GetScreenshot());
            ShowTempStatus("Screenshot copied to clipboard!");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentScreenshot == null) return;

            SaveBitmapWithDialog(GetScreenshot());
        }

        private void radius_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void Maximize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

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

        private void PaddingPresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaddingPresetComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Tag.ToString(), out int padding))
                {
                    if (padding >= 0) // Not "Custom"
                    {
                        ScreenshotImage.Margin = new Thickness(padding);

                        // Update slider if you have one
                        if (PaddingSlider != null)
                        {
                            PaddingSlider.Value = padding;
                        }
                    }
                    // If "Custom" is selected, do nothing - let user use slider
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWatermarkSize();
        }

    }
}