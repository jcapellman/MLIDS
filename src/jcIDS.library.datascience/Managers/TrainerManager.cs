using System.IO;

using jcIDS.library.core.Managers;
using jcIDS.library.datascience.ModelObjects;

using Microsoft.ML.Data;

namespace jcIDS.library.datascience.Managers
{
    public class TrainerManager
    {
        public bool TrainModel(string dbFileName)
        {
            var modelFileName = $"{dbFileName}.model";

            var networkData = new NetworkDeviceManager().ToCSV();

            File.WriteAllText(modelFileName, networkData);

            var pipeline = new LearningPipeline
            {
                new TextLoader(modelFileName).CreateFrom<NetworkModelObject>(separator: ','),
                new Dictionarizer("Label"),
                new ColumnConcatenator("IPAddress"),
                new FastTreeBinaryClassifier(),
                new PredictedLabelColumnOriginalValueConverter() {PredictedLabelColumn = "PredictedLabel"}
            };
            
            var model = pipeline.Train<NetworkModelObject, NetworkPredictionObject>();

            return model != null;
        }
    }
}