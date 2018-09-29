using System;

using jcIDS.library.datascience.Managers;

namespace jcIDS.DataScience.ModelTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify the LiteDB filename to train on");

                return;
            }

            var result = new TrainerManager().TrainModel(args[0]);

            Console.WriteLine(result ? "Model trained successfully" : "Model Training failed");
        }
    }
}