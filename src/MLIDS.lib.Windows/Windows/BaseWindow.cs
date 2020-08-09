using MLIDS.lib.Common;
using MLIDS.lib.Windows.ViewModels;

using System;
using System.Linq;
using System.Windows;

namespace MLIDS.lib.Windows
{
    public class BaseWindow : Window
    {
        public BaseViewModel Vm => (BaseViewModel)DataContext;

        public void Vm_OnFailedDAL(object sender, string dalName)
        {
            MessageBox.Show($"Failed to initialize the DAL ({dalName}) - check your settings in ({Constants.SETTINGS_FILENAME})");
        }

        protected override void OnActivated(EventArgs e)
        {
            Vm.OnFailedDAL += Vm_OnFailedDAL;

            SanityCheckDriver();

            base.OnActivated(e);
        }

        public void SanityCheckDriver()
        {
            if (Vm.DeviceList.Any())
            {
                return;
            }

            MessageBox.Show(Constants.MESSAGE_NPCAP_NOT_FOUND);

            Application.Current.Shutdown();
        }

        public void btnSaveSettings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Vm.SaveSettings();

                MessageBox.Show(Constants.MESSAGE_SAVE_SETTINGS);
            } catch (Exception ex)
            {
                MessageBox.Show($"Error while saving Settings: {ex}");
            }
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