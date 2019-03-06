using System.IO;

using jcIDS.library.core.Managers;

using Microsoft.ML;

namespace jcIDS.library.datascience.Managers
{
    public class TrainerManager { 
        protected MLContext MlContext;

        public TrainerManager()
        {
            MlContext = new MLContext(seed: 0);
        }

        public bool TrainModel(string dbFileName)
        {
            var dataFileName = $"{dbFileName}.csv";
            var modelFileName = "jcids.mdl";

            var networkData = new NetworkDeviceManager().ToCSV();

            File.WriteAllText(dataFileName, networkData);

            var textReader = MlContext.Data.LoadFromTextFile(dataFileName);

            var pipeline = MlContext.Transforms.Text.FeaturizeText("Content", "Features")
                .Append(MlContext.BinaryClassification.Trainers.FastTree(numLeaves: 2, numTrees: 10, minDatapointsInLeaves: 1));

            var trainedModel = pipeline.Fit(textReader);

            using (var fs = File.Create(modelFileName))
            {
                trainedModel.SaveTo(MlContext, fs);
            }

            return true;
        }
    }
}