using System.IO;

using jcIDS.lib.CommonObjects;
using jcIDS.ML.lib.Objects;

using Microsoft.ML;

namespace jcIDS.ML.lib
{
    public class Prediction
    {
        private MLContext _mlContext;

        public Prediction()
        {
            _mlContext = new MLContext();
        }

        public bool Predict(byte[] model, Packet packet)
        {
            ITransformer trainedModel;

            using (var ms = new MemoryStream(model))
            {
                trainedModel = _mlContext.Model.Load(ms);
            }

            var predFunction = trainedModel.CreatePredictionEngine<Packet, PredictionResult>(_mlContext);

            var prediction = predFunction.Predict(packet);

            return prediction.Malicious;
        }
    }
}
