using MLIDS.DataCapture.ViewModels;

using System.Windows;

namespace MLIDS.DataCapture
{
    public partial class MainWindow : Window
    {
        private MainViewModel VM => (MainViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();                        
        }

        private void btnStartCapturing_Click(object sender, RoutedEventArgs e)
        {
            VM.StartCapture();
        }

        private void btnStopCapturing_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}