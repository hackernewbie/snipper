using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;

namespace Snipper
{
    public partial class App : Application
    {
        // DPI Awareness levels
        private enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDpiAware();

        public App()
        {
            SetDpiAwareness();
        }

        private void SetDpiAwareness()
        {
            try
            {
                // Try the newer API first (Windows 8.1+)
                SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
            }
            catch (DllNotFoundException)
            {
                // Fall back to older API (Windows Vista+)
                SetProcessDpiAware();
            }
            catch (Exception)
            {
                // If both fail, continue without DPI awareness
                // Your screenshot scaling will still work, just less optimal
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
