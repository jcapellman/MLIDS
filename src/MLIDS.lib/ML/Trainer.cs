using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

using MLIDS.lib.Objects;
using static Microsoft.ML.DataOperationsCatalog;

namespace MLIDS.lib.ML
{
    public class Trainer
    {
        private readonly MLContext _mlContext;

        public Trainer()
        {
            _mlContext = new MLContext(2020);
        }

        private TrainTestData GetDataView(string fileName, bool training = true)
        {
            var trainingDataView = _mlContext.Data.LoadFromTextFile<PayloadItem>(fileName, ',');

            return _mlContext.Data.TrainTestSplit(trainingDataView, seed: 2020);
        }

        public AnomalyDetectionMetrics GenerateModel(string trainingFileName, string modelFileName)
        {
            var options = new RandomizedPcaTrainer.Options
            {
                ExampleWeightColumnName = null,
                Rank = 5,
                Oversampling = 20,
                EnsureZeroMean = true,
                Seed = 1
            };

            var data = GetDataView(trainingFileName);

            IEstimator<ITransformer> trainer = _mlContext.AnomalyDetection.Trainers.RandomizedPca(options: options);

            ITransformer trainedModel = trainer.Fit(data.TrainSet);

            _mlContext.Model.Save(trainedModel, data.TrainSet.Schema, modelFileName);

            var testSetTransform = trainedModel.Transform(data.TestSet);

            return _mlContext.AnomalyDetection.Evaluate(testSetTransform);
        }
    }
}