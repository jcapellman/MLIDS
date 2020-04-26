using System.Windows;

using MLIDS.Detector.ViewModels;
using MLIDS.lib.Windows;

namespace MLIDS.Detector
{
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();       
        }

        private void btnModelLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainViewModel)Vm).SelectModelFile())
            {
                MessageBox.Show("Model file could not be loaded");
            }
        }
    }
}