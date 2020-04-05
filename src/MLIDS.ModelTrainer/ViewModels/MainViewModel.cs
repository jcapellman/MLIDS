using System.ComponentModel;
using Microsoft.Win32;

namespace MLIDS.ModelTrainer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _locationCleanTrafficFile;

        public string LocationCleanTrafficFile
        {
            get => _locationCleanTrafficFile;

            set
            {
                _locationCleanTrafficFile = value;

                OnPropertyChanged();

                UpdateTrainButton();
            }
        }

        private string _locationMaliciousTrafficFile;

        public string LocationMaliciousTrafficFile
        {
            get => _locationMaliciousTrafficFile;

            set
            {
                _locationMaliciousTrafficFile = value;

                OnPropertyChanged();

                UpdateTrainButton();
            }
        }

        private string _locationModelFile;

        public string LocationModelFile
        {
            get => _locationModelFile;

            set
            {
                _locationModelFile = value;

                OnPropertyChanged();

                UpdateTrainButton();
            }
        }

        private bool _btnTrainEnable;

        public bool btnTrainEnable
        {
            get => _btnTrainEnable;

            set
            {
                _btnTrainEnable = value;

                OnPropertyChanged();
            }
        }

        private bool _isTraining = false;

        private void UpdateTrainButton()
        {
            btnTrainEnable = !string.IsNullOrEmpty(LocationCleanTrafficFile) &&
                             !string.IsNullOrEmpty(LocationMaliciousTrafficFile) &&
                             !string.IsNullOrEmpty(LocationModelFile) &&
                             !_isTraining;
        }

        public MainViewModel()
        {
            UpdateTrainButton();
        }

        public void SelectModelSaveOutput()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "Model|*.mdl",
                Title = "Save Model"
            };

            saveDialog.ShowDialog();

            if (string.IsNullOrEmpty(saveDialog.FileName))
            {
                return;
            }

            LocationModelFile = saveDialog.FileName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}