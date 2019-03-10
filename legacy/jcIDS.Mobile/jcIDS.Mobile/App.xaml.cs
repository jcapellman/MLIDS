using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using jcIDS.Mobile.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace jcIDS.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            MainPage = new MainPage();
        }
    }
}