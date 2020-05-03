using System;
using System.Windows;

using Microsoft.Win32;

using MLIDS.lib.Containers;
using MLIDS.lib.ML;
using MLIDS.lib.Windows.ViewModels;

namespace MLIDS.ModelTrainer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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
            StartBtnEnabled =
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

            btnSelectionEnable = true;

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

        public override async void StartAction()
        {
            if (_dataStorage == null)
            {
                throw new NullReferenceException("Data Storage was null");
            }

            if (string.IsNullOrEmpty(LocationModelFile))
            {
                throw new NullReferenceException("Location Model FileName was null");
            }

            IsRunning = true;

            ModelMetrics = await _trainer.GenerateModel(_dataStorage, LocationModelFile);

            ModelTrainingDuration = $"{ModelMetrics.Duration.TotalSeconds} seconds";

            ModelMetricsStackPanel = Visibility.Visible;

            IsRunning = false;
        }

        public override void StopAction()
        {
            // Not Used
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