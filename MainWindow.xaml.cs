using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using Size = System.Windows.Size;

namespace Snipper
{
    public partial class MainWindow : Window
    {
        private BitmapSource? _currentScreenshot;

        public MainWindow()
        {
            InitializeComponent();
            InitializeImageEffect();
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
            _currentScreenshot = screenshot;
            ScreenshotImage.Source = screenshot;
            PlaceholderText.Visibility = Visibility.Collapsed;

            PlaceHolderBorder.Visibility = Visibility.Collapsed;

            // Enable copy and save buttons
            CopyButton.IsEnabled = true;
            SaveButton.IsEnabled = true;
        }

        private void BackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string backgroundType = button?.Tag?.ToString() ?? "";

            switch (backgroundType)
            {
                case "BlueGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(227, 242, 253), // light blue
                        System.Windows.Media.Color.FromRgb(100, 181, 246), // mid blue
                        new System.Windows.Point(0.5, 0), // top center
                        new System.Windows.Point(0.5, 1)); // bottom center
                    break;
                case "PurpleGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(243, 229, 245), // soft lavender
                        System.Windows.Media.Color.FromRgb(149, 117, 205), // soft purple
                        new System.Windows.Point(0.5, 0),
                        new System.Windows.Point(0.5, 1));
                    break;
                case "OrangeGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(255, 244, 229), // warm peach base
                        System.Windows.Media.Color.FromRgb(255, 183, 77),  // soft amber
                        new System.Windows.Point(0.5, 0),
                        new System.Windows.Point(0.5, 1));
                    break;
                case "GrayGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(230, 230, 230), // light gray
                        System.Windows.Media.Color.FromRgb(74,74,74), // slightly darker gray
                        new System.Windows.Point(0.5, 0),
                        new System.Windows.Point(0.5, 1));
                    break;
                case "BlackGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(230, 230, 230),   // dark gray
                        System.Windows.Media.Color.FromRgb(13, 13, 13),   // near black
                        new System.Windows.Point(0.5, 0),
                        new System.Windows.Point(0.5, 1));
                    break;
                case "Transparent":
                    BackgroundBorder.Background = new SolidColorBrush(Colors.Transparent);
                    break;
            }
        }

        
        private RenderTargetBitmap CaptureAtHighRes(FrameworkElement target, double scale)
        {
            var originalTransform = target.LayoutTransform;

            try
            {
                var dpi = VisualTreeHelper.GetDpi(target);

                // Apply scale
                var scaleTransform = new ScaleTransform(scale, scale);
                target.LayoutTransform = scaleTransform;

                // Layout pass
                target.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                target.Arrange(new Rect(target.DesiredSize));
                target.UpdateLayout();

                // Force render pass
                target.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => { }));

                // Capture
                var size = target.DesiredSize;
                int pixelWidth = (int)(size.Width * dpi.DpiScaleX);
                int pixelHeight = (int)(size.Height * dpi.DpiScaleY);

                var rtb = new RenderTargetBitmap(pixelWidth, pixelHeight, dpi.PixelsPerInchX, dpi.PixelsPerInchY, PixelFormats.Pbgra32);
                rtb.Render(target);

                return rtb;
            }
            finally
            {
                target.LayoutTransform = originalTransform;
                target.UpdateLayout();

            }
        }

        // Returns a high‑res snapshot of ScreenshotContainer.
        private RenderTargetBitmap GetScreenshot()
        {
            // If the Viewbox is using Stretch="Uniform", 1× logical scale is enough
            const double SCALE = 1.0;
            return CaptureAtHighRes(ScreenshotContainer, SCALE);
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
            }
        }
        private void InsetSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (BackgroundBorder != null)
            {
                double inset = InsetSlider.Value;
                ScreenshotContainer.Margin = new Thickness(inset);
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

        private void VPaddingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}