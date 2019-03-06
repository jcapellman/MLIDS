using Microsoft.ML.Data;

namespace jcIDS.library.datascience.ModelObjects
{
    public class NetworkPredictionObject
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}