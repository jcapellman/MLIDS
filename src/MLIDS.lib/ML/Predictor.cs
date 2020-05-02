using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using Microsoft.ML;

using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.ML
{
    public class Predictor
    {
        private readonly PredictionEngine<PayloadItem, PredictionItem> _predictionEngine;

        public Predictor([NotNull]string modelName)
        {
            if (string.IsNullOrEmpty(modelName))
            {
                throw new ArgumentNullException(nameof(modelName));
            }

            if (!File.Exists(modelName))
            {
                throw new FileNotFoundException(nameof(modelName));
            }

            var mlContext = new MLContext(2020);

            var model = mlContext.Model.Load(modelName, out _);

            _predictionEngine = mlContext.Model.CreatePredictionEngine<PayloadItem, PredictionItem>(model);
        }

        public PredictionItem Predict(PayloadItem item) => _predictionEngine.Predict(item);
    }
}