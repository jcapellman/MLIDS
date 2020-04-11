using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

using MLIDS.lib.Containers;
using MLIDS.lib.Extensions;
using MLIDS.lib.ML.Objects;
using static Microsoft.ML.DataOperationsCatalog;

namespace MLIDS.lib.ML
{
    public class Trainer
    {
        protected const string FEATURES = "Features";

        private readonly MLContext _mlContext;

        public Trainer()
        {
            _mlContext = new MLContext(2020);
        }

        private (TrainTestData Data, int cleanRowCount, int maliciousRowCount) GetDataView(string cleanFileName, string maliciousFileName)
        {
            var cleanFileData = File.ReadAllLines(cleanFileName);
            var maliciousFileData = File.ReadAllLines(maliciousFileName);

            var fileName = Path.GetRandomFileName();

            var combinedData = new List<string>();

            combinedData.AddRange(cleanFileData);
            combinedData.AddRange(maliciousFileData);

            File.WriteAllLines(fileName, combinedData);

            var trainingDataView = _mlContext.Data.LoadFromTextFile<PayloadItem>(fileName, ',');

            return (_mlContext.Data.TrainTestSplit(trainingDataView, seed: 2020), cleanFileData.Length, maliciousFileData.Length);
        }

        public ModelMetrics GenerateModel(string cleanFileName, string maliciousFileName, string modelFileName)
        {
            var startTime = DateTime.Now;

            var options = new RandomizedPcaTrainer.Options
            {
                FeatureColumnName = FEATURES,
                ExampleWeightColumnName = null,
                Rank = 5,
                Oversampling = 20,
                EnsureZeroMean = true,
                Seed = 2020
            };

            var (data, cleanRowCount, maliciousRowCount) = GetDataView(cleanFileName, maliciousFileName);

            IEstimator<ITransformer> dataProcessPipeline = _mlContext.Transforms.Concatenate(
                FEATURES,
                typeof(PayloadItem).ToPropertyList<PayloadItem>(nameof(PayloadItem.Label)));

            IEstimator<ITransformer> trainer = _mlContext.AnomalyDetection.Trainers.RandomizedPca(options: options);

            EstimatorChain<ITransformer> trainingPipeline = dataProcessPipeline.Append(trainer);

            TransformerChain<ITransformer> trainedModel = trainingPipeline.Fit(data.TrainSet);

            _mlContext.Model.Save(trainedModel, data.TrainSet.Schema, modelFileName);

            var testSetTransform = trainedModel.Transform(data.TestSet);

            return new ModelMetrics
            {
                Metrics = _mlContext.AnomalyDetection.Evaluate(testSetTransform),
                NumCleanRows = cleanRowCount,
                NumMaliciousRows = maliciousRowCount,
                Duration = DateTime.Now.Subtract(startTime)
            };
        }
    }
}