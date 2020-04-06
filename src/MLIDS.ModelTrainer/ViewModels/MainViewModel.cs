using System.ComponentModel;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

using Microsoft.Win32;
using MLIDS.lib.ML;
using MLIDS.lib.Objects;

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

        private bool _isTraining;

        public bool IsTraining
        {
            get => _isTraining;

            set
            {
                _isTraining = value;

                OnPropertyChanged();

                btnSelectionEnable = !value;
            }
        }

        private void UpdateTrainButton()
        {
            btnTrainEnable = !string.IsNullOrEmpty(LocationCleanTrafficFile) &&
                             !string.IsNullOrEmpty(LocationMaliciousTrafficFile) &&
                             !string.IsNullOrEmpty(LocationModelFile) &&
                             !IsTraining;
        }

        private bool _btnSelectionEnable;

        public bool btnSelectionEnable
        {
            get => _btnSelectionEnable;

            set
            {
                _btnSelectionEnable = value;

                OnPropertyChanged();
            }
        }

        private AnomalyDetectionMetrics _modelMetrics;

        public AnomalyDetectionMetrics ModelMetrics
        {
            get => _modelMetrics;

            set
            {
                _modelMetrics = value;

                OnPropertyChanged();
            }
        }

        private readonly Trainer _trainer = new Trainer();

        public MainViewModel()
        {
            IsTraining = false;

            UpdateTrainButton();
        }

        public void TrainModel()
        {
            IsTraining = true;

            ModelMetrics = _trainer.GenerateModel(LocationCleanTrafficFile, LocationModelFile);

            IsTraining = false;
        }

        public void SelectMaliciousFileInput()
        {
            LocationMaliciousTrafficFile = SelectInputFile() ?? LocationMaliciousTrafficFile;
        }

        public void SelectCleanFileInput()
        {
            LocationCleanTrafficFile = SelectInputFile() ?? LocationCleanTrafficFile;
        }

        private string SelectInputFile()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "Network Traffic|*.csv",
                Title = "Select Network Traffic"
            };

            openDialog.ShowDialog();

            return openDialog.FileName;
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