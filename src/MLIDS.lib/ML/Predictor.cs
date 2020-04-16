using Microsoft.ML;

using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.ML
{
    public class Predictor
    {
        private readonly PredictionEngine<PayloadItem, PredictionItem> _predictionEngine;

        public Predictor(string modelName)
        {
            var mlContext = new MLContext(2020);

            var model = mlContext.Model.Load(modelName, out _);

            _predictionEngine = mlContext.Model.CreatePredictionEngine<PayloadItem, PredictionItem>(model);
        }

        public PredictionItem Predict(PayloadItem item) => _predictionEngine.Predict(item);
    }
}