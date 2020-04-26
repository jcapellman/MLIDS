using MLIDS.lib.ViewModels;

using System.Linq;
using System.Windows;

namespace MLIDS.lib.Windows
{
    public class BaseWindow : Window
    {
        public BaseViewModel Vm => (BaseViewModel)DataContext;

        public void SanityCheckDriver()
        {
            if (Vm.DeviceList.Any())
            {
                return;
            }

            MessageBox.Show("NPCAP Driver not installed - please install (https://nmap.org/npcap/)");

            Application.Current.Shutdown();
        }

        public void btnStartAction_Click(object sender, RoutedEventArgs e)
        {
            Vm.StartAction();
        }

        public void btnStopAction_Click(object sender, RoutedEventArgs e)
        {
            Vm.StopAction();
        }
    }
}