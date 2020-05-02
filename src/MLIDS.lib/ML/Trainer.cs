using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using static Microsoft.ML.DataOperationsCatalog;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.Extensions;
using MLIDS.lib.ML.Objects;
using System.IO;

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

        private (TrainTestData Data, int cleanRowCount, int maliciousRowCount) GetDataView(List<PayloadItem> cleanData, List<PayloadItem> maliciousData)
        {
            var cleanDataLength = cleanData.Count;
            var maliciousDataLength = maliciousData.Count;

            cleanData.AddRange(maliciousData);

            var trainingDataView = _mlContext.Data.LoadFromEnumerable(cleanData);

            return (_mlContext.Data.TrainTestSplit(trainingDataView, seed: 2020), cleanDataLength, maliciousDataLength);
        }

        public async Task<ModelMetrics> GenerateModel(BaseDAL storage, string modelFileName)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            if (string.IsNullOrEmpty(modelFileName))
            {
                throw new ArgumentNullException(nameof(modelFileName));
            }

            if (!File.Exists(modelFileName))
            {
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
                Seed = 2020
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