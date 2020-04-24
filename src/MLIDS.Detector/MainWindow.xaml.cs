using System.Linq;
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

            if (Vm.DeviceList.Any())
            {
                return;
            }

            MessageBox.Show("NDIS Driver not installed - please install");
        }

        private void btnStartCapturing_Click(object sender, RoutedEventArgs e)
        {
            Vm.StartCapture();
        }

        private void btnModelLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!Vm.SelectModelFile())
            {
                MessageBox.Show("Model file could not be loaded");
            }
        }

        private void btnStopCapturing_Click(object sender, RoutedEventArgs e)
        {
            Vm.StopCapture();
        }
    }
}