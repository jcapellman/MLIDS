using System.Windows;

using MLIDS.lib.Windows;
using MLIDS.ModelTrainer.ViewModels;

namespace MLIDS.ModelTrainer
{
    public partial class MainWindow : BaseWindow
    {
        private MainViewModel ViewModel => (MainViewModel) DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCleanLocation_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectCleanFileInput();
        }

        private void btnModelOutput_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectModelSaveOutput();
        }

        private void btnMaliciousLocation_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectMaliciousFileInput();
        }
    }
}