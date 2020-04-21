using System.Windows;

using MLIDS.Detector.ViewModels;

namespace MLIDS.Detector
{
    public partial class MainWindow : Window
    {
        private MainViewModel Vm => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCleanLocation_Click(object sender, RoutedEventArgs e)
        {
            Vm.SelectModelFile();
        }

        private void btnStartCapturing_Click(object sender, RoutedEventArgs e)
        {
            Vm.StartCapture();
        }

        private void btnStopCapturing_Click(object sender, RoutedEventArgs e)
        {
            Vm.StopCapture();
        }
    }
}