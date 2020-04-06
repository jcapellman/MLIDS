using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

using MLIDS.lib.Objects;

namespace MLIDS.lib.ML
{
    public class Trainer
    {
        private readonly MLContext _mlContext;

        public Trainer()
        {
            _mlContext = new MLContext(2020);
        }

        private (IDataView DataView, IEstimator<ITransformer> Transformer) GetDataView(string fileName, bool training = true)
        {
            var trainingDataView = _mlContext.Data.LoadFromTextFile<PayloadItem>(fileName, ',');

            return (trainingDataView, null);

            //      IEstimator<ITransformer> dataProcessPipeline = MlContext.Transforms.Concatenate(
            //          FEATURES,
            //           typeof(LoginHistory).ToPropertyList<LoginHistory>(nameof(LoginHistory.Label)));

            //     return (trainingDataView, dataProcessPipeline);
        }


        public AnomalyDetectionMetrics GenerateModel(string trainingFileName, string modelFileName)
        {
            var options = new RandomizedPcaTrainer.Options
            {
                //     FeatureColumnName = FEATURES,
                ExampleWeightColumnName = null,
                Rank = 5,
                Oversampling = 20,
                EnsureZeroMean = true,
                Seed = 1
            };

            var trainingDataView = GetDataView(trainingFileName);

            IEstimator<ITransformer> trainer = _mlContext.AnomalyDetection.Trainers.RandomizedPca(options: options);

            EstimatorChain<ITransformer> trainingPipeline = trainingDataView.Transformer.Append(trainer);

            TransformerChain<ITransformer> trainedModel = trainingPipeline.Fit(trainingDataView.DataView);

            _mlContext.Model.Save(trainedModel, trainingDataView.DataView.Schema, modelFileName);

            var testingDataView = GetDataView(trainingFileName, true);

            var testSetTransform = trainedModel.Transform(testingDataView.DataView);

            return _mlContext.AnomalyDetection.Evaluate(testSetTransform);
        }
    }
}