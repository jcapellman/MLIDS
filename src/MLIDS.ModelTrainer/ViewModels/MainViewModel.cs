using System.ComponentModel;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

using Microsoft.Win32;

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

        public MainViewModel()
        {
            IsTraining = false;

            UpdateTrainButton();
        }

        private MLContext MlContext = new MLContext(2020);

        private (IDataView DataView, IEstimator<ITransformer> Transformer) GetDataView(string fileName, bool training = true)
        {
            var trainingDataView = MlContext.Data.LoadFromTextFile<PayloadItem>(fileName, ',');

                return (trainingDataView, null);
  
      //      IEstimator<ITransformer> dataProcessPipeline = MlContext.Transforms.Concatenate(
      //          FEATURES,
     //           typeof(LoginHistory).ToPropertyList<LoginHistory>(nameof(LoginHistory.Label)));

       //     return (trainingDataView, dataProcessPipeline);
        }

        public void TrainModel()
        {
            IsTraining = true;

            var options = new RandomizedPcaTrainer.Options
            {
           //     FeatureColumnName = FEATURES,
                ExampleWeightColumnName = null,
                Rank = 5,
                Oversampling = 20,
                EnsureZeroMean = true,
                Seed = 1
            };

            var trainingDataView = GetDataView(LocationCleanTrafficFile);

            IEstimator<ITransformer> trainer = MlContext.AnomalyDetection.Trainers.RandomizedPca(options: options);

            EstimatorChain<ITransformer> trainingPipeline = trainingDataView.Transformer.Append(trainer);

            TransformerChain<ITransformer> trainedModel = trainingPipeline.Fit(trainingDataView.DataView);

            MlContext.Model.Save(trainedModel, trainingDataView.DataView.Schema, LocationModelFile);

            var testingDataView = GetDataView(LocationCleanTrafficFile, true);

            var testSetTransform = trainedModel.Transform(testingDataView.DataView);

            ModelMetrics = MlContext.AnomalyDetection.Evaluate(testSetTransform);

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