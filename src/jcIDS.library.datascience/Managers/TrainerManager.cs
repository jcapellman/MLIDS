using jcIDS.library.datascience.ModelObjects;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace jcIDS.library.datascience.Managers
{
    public class TrainerManager
    {
        public bool TrainModel(string dbFileName)
        {
            var pipeline = new LearningPipeline();
            
            pipeline.Add(new TextLoader(dbFileName).CreateFrom<NetworkModelObject>(separator: ','));

            pipeline.Add(new Dictionarizer("Label"));

            pipeline.Add(new ColumnConcatenator("IPAddress"));
            
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());

            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });
            
            var model = pipeline.Train<NetworkModelObject, NetworkPredictionObject>();

            return model != null;
        }
    }
}