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
                        System.Windows.Media.Color.FromRgb(25, 118, 210),
                        System.Windows.Media.Color.FromRgb(66, 165, 245),
                        new System.Windows.Point(0, 0),
                        new System.Windows.Point(1, 1));
                    break;
                case "PurpleGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(123, 31, 162),
                        System.Windows.Media.Color.FromRgb(186, 104, 200),
                        new System.Windows.Point(0, 0),
                        new System.Windows.Point(1, 1));
                    break;
                case "OrangeGradient":
                    BackgroundBorder.Background = new LinearGradientBrush(
                        System.Windows.Media.Color.FromRgb(245, 124, 0),
                        System.Windows.Media.Color.FromRgb(255, 183, 77),
                        new System.Windows.Point(0, 0),
                        new System.Windows.Point(1, 1));
                    break;
                case "White":
                    BackgroundBorder.Background = new SolidColorBrush(Colors.White);
                    break;
                case "Black":
                    BackgroundBorder.Background = new SolidColorBrush(Colors.Black);
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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentScreenshot != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg",
                    DefaultExt = "png",
                    FileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}"
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
        }
    }
}