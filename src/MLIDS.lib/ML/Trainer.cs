using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using static Microsoft.ML.DataOperationsCatalog;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.Extensions;
using MLIDS.lib.ML.Objects;
using MLIDS.lib.Common;

namespace MLIDS.lib.ML
{
    public class Trainer
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        protected const string FEATURES = "Features";

        private readonly MLContext _mlContext;

        public Trainer()
        {
            _mlContext = new MLContext(Constants.ML_SEED);
        }

        private (TrainTestData Data, int cleanRowCount, int maliciousRowCount) GetDataView(List<PayloadItem> cleanData, List<PayloadItem> maliciousData)
        {
            var cleanDataLength = cleanData.Count;
            var maliciousDataLength = maliciousData.Count;

            cleanData.AddRange(maliciousData);

            var trainingDataView = _mlContext.Data.LoadFromEnumerable(cleanData);

            return (_mlContext.Data.TrainTestSplit(trainingDataView, seed: Constants.ML_SEED), cleanDataLength, maliciousDataLength);
        }

        public async Task<ModelMetrics> GenerateModel(BaseDAL storage, string modelFileName)
        {
            if (storage == null)
            {
                Log.Error("Trainer::GenerateModel - BaseDAL is null");

                throw new ArgumentNullException(nameof(storage));
            }

            if (string.IsNullOrEmpty(modelFileName))
            {
                Log.Error("Trainer::GenerateModel - modelFileName is null");

                throw new ArgumentNullException(nameof(modelFileName));
            }

            if (!File.Exists(modelFileName))
            {
                Log.Error($"Trainer::GenerateModel - {modelFileName} does not exist");

                throw new FileNotFoundException(modelFileName);
            }

            var startTime = DateTime.Now;

            var options = new RandomizedPcaTrainer.Options
            {
                FeatureColumnName = FEATURES,
                ExampleWeightColumnName = null,
                Rank = 4,
                Oversampling = 20,
                EnsureZeroMean = true,
                Seed = Constants.ML_SEED
            };

            var (data, cleanRowCount, maliciousRowCount) = GetDataView(await storage.QueryPacketsAsync(a => a.IsClean), await storage.QueryPacketsAsync(a => !a.IsClean));

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