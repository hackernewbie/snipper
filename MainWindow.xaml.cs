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

namespace Snipper
{
    public partial class MainWindow : Window
    {
        private BitmapSource? _currentScreenshot;

        public MainWindow()
        {
             InitializeComponent();
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

        private async void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentScreenshot != null)
            {
                // Create a visual from the screenshot container
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                    (int)ScreenshotContainer.ActualWidth,
                    (int)ScreenshotContainer.ActualHeight,
                    96, 96, PixelFormats.Pbgra32);

                renderBitmap.Render(ScreenshotContainer);

                Clipboard.SetImage(renderBitmap);
                
                string originalTitle = this.Title;
                this.Title = "Screenshot copied to clipboard successfully!";

                await Task.Delay(3000);
                this.Title = originalTitle;
                
                //MessageBox.Show("Screenshot copied to clipboard!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string originalTitle = this.Title;
                string newMessage = "Nothing to copy!\n Please take a screenshot first!";
                this.Title = newMessage;

                PlaceholderText.Text = newMessage;

                await Task.Delay(3000);
                this.Title = originalTitle;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentScreenshot != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg",
                    DefaultExt = "png",
                    FileName = $"Snipper_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveDialog.ShowDialog() == true)
                {
                    // Create a visual from the screenshot container
                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                        (int)ScreenshotContainer.ActualWidth,
                        (int)ScreenshotContainer.ActualHeight,
                        96, 96, PixelFormats.Pbgra32);

                    renderBitmap.Render(ScreenshotContainer);

                    BitmapEncoder encoder = saveDialog.FilterIndex == 1 ?
                        new PngBitmapEncoder() : new JpegBitmapEncoder();

                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                    using (FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create))
                    {
                        encoder.Save(stream);
                    }

                    MessageBox.Show($"Screenshot saved to {saveDialog.FileName}", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                string originalTitle = this.Title;
                string newMessage = "Nothing to save!\n Please take a screenshot first!";
                this.Title = newMessage;

                PlaceholderText.Text = newMessage;

                await Task.Delay(3000);
                this.Title = originalTitle;
            }
        }

        private void radius_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

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
    }
}