using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.ML.Data;

using Microsoft.Win32;
using MLIDS.lib.Containers;
using MLIDS.lib.ML;
using MLIDS.lib.ViewModels;

namespace MLIDS.ModelTrainer.ViewModels
{
    public class MainViewModel : BaseViewModel
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

        private void UpdateTrainButton()
        {
            StartBtnEnabled = !string.IsNullOrEmpty(LocationCleanTrafficFile) &&
                             !string.IsNullOrEmpty(LocationMaliciousTrafficFile) &&
                             !string.IsNullOrEmpty(LocationModelFile) &&
                             !IsRunning;
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

        private ModelMetrics _modelMetrics;

        public ModelMetrics ModelMetrics
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
            ModelMetricsStackPanel = Visibility.Collapsed;

            UpdateTrainButton();
        }

        private string _modelTrainingDuration;

        public string ModelTrainingDuration
        {
            get => _modelTrainingDuration;

            set
            {
                _modelTrainingDuration = value;

                OnPropertyChanged();
            }
        }

        private Visibility _modelMetricsStackPanel;

        public Visibility ModelMetricsStackPanel
        {
            get => _modelMetricsStackPanel;

            set
            {
                _modelMetricsStackPanel = value;

                OnPropertyChanged();
            }
        }

        public void TrainModel()
        {
            IsRunning = true;

            ModelMetrics = _trainer.GenerateModel(LocationCleanTrafficFile, LocationMaliciousTrafficFile, LocationModelFile);

            ModelTrainingDuration = $"{ModelMetrics.Duration.TotalSeconds} seconds";

            ModelMetricsStackPanel = Visibility.Visible;

            IsRunning = false;
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
    }
}