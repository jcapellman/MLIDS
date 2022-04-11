using System;
using System.Windows;

namespace MLIDS.DataCapture
{
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.GetType() == typeof(DllNotFoundException))
            {
                MessageBox.Show("WinPcap is not installed");
            }
        }
    }
}