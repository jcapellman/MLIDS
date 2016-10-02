using Windows.UI.Xaml.Controls;

using jcIDS.Service.UWP.ViewModel;

namespace jcIDS.Service.UWP {
    public sealed partial class MainPage : Page {
        private MainPageViewModel viewModel => (MainPageViewModel)DataContext;

        public MainPage() {
            this.InitializeComponent();

            DataContext = new MainPageViewModel();
        }
    }
}