using System.Collections.ObjectModel;

namespace jcIDS.Service.UWP.ViewModel {
    public class MainPageViewModel : BaseViewModel {
        private ObservableCollection<string> _messageLog;

        public ObservableCollection<string> MessageLog {
            get { return _messageLog; }
            set { _messageLog = value; OnPropertyChanged(); }
        }

        private void AddMessage(string message) {
            MessageLog.Add(message);
        }

        public MainPageViewModel() {
            MessageLog = new ObservableCollection<string>();

            var mainService = new MainService();

            if (!mainService.Initialize()) {
                AddMessage("Failed to Initialize - shutting down");

                return;
            }

            mainService.Run();
        }
    }
}