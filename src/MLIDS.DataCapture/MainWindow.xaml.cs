using System;
using System.Windows;

using MLIDS.DataCapture.ViewModels;
using MLIDS.lib.Windows;

namespace MLIDS.DataCapture
{
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                DataContext = new MainViewModel();
            } catch (DllNotFoundException e)
            {
                if (e.Message.Contains("wpcap"))
                {
                    MessageBox.Show("WinPcap is not installed and is required to run the application - exiting");

                    Environment.Exit(1);
                }                
            }
        }
    }
}