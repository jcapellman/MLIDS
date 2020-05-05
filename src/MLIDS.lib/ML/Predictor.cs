using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using Microsoft.ML;

using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.ML
{
    public class Predictor
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private readonly PredictionEngine<PayloadItem, PredictionItem> _predictionEngine;

        public Predictor([NotNull]string modelName)
        {
            if (string.IsNullOrEmpty(modelName))
            {
                Log.Error("Predictor - modelName was null or empty");

                throw new ArgumentNullException(nameof(modelName));
            }

            if (!File.Exists(modelName))
            {
                Log.Error($"Predictor - {modelName} file not found");

                throw new FileNotFoundException(nameof(modelName));
            }

            var mlContext = new MLContext(2020);

            var model = mlContext.Model.Load(modelName, out _);

            _predictionEngine = mlContext.Model.CreatePredictionEngine<PayloadItem, PredictionItem>(model);
        }

        public PredictionItem Predict(PayloadItem item) => _predictionEngine.Predict(item);
    }
}