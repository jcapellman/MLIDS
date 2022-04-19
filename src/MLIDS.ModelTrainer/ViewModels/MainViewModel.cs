using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Microsoft.Win32;

using MLIDS.lib.Containers;
using MLIDS.lib.ML;
using MLIDS.lib.Models.Base;
using MLIDS.lib.Windows.ViewModels;

namespace MLIDS.ModelTrainer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

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

        private List<BaseModelRunner> _availableRunners;

        public List<BaseModelRunner> AvailableRunners
        {
            get => _availableRunners;

            set
            {
                _availableRunners = value;

                OnPropertyChanged();
            }
        }

        private BaseModelRunner _selectedRunner;

        public BaseModelRunner SelectedRunner
        {
            get => _selectedRunner;

            set
            {
                _selectedRunner = value;

                OnPropertyChanged();
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

            AvailableRunners = lib.Helpers.ModelRunnerHelper.GetAvailableRunners();

            SelectedRunner = AvailableRunners.FirstOrDefault();

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
            if (SelectedDataLayer == null)
            {
                Log.Error("MainViewModel::StartAction - Data Storage was null");

                throw new NullReferenceException("Data Storage was null");
            }

            if (string.IsNullOrEmpty(LocationModelFile))
            {
                Log.Error("MainViewModel::StartAction - LocationModelFile was null or empty");

                throw new NullReferenceException("Location Model FileName was null");
            }

            IsRunning = true;

            ModelMetrics = await _trainer.GenerateModel(SelectedDataLayer, LocationModelFile);

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

        public override void StartButtonEnablement()
        {
            StartBtnEnabled = !string.IsNullOrEmpty(LocationModelFile) &&
                                SelectedDataLayer.IsSelectable &&
                                !IsRunning;
        }
    }
}