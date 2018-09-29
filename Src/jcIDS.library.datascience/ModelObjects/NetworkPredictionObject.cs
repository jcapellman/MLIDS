using Microsoft.ML.Runtime.Api;

namespace jcIDS.library.datascience.ModelObjects
{
    public class NetworkPredictionObject
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}