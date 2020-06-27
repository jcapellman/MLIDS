using MLIDS.ScriptEditor.ViewModels;

using System.Windows;

namespace MLIDS.ScriptEditor
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewScript();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenScript();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveScript();
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveAsScript();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.Exit())
            {
                return;
            }

            Application.Current.Shutdown();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}