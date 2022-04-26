using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.ModelTrainer
{
    [TestClass]
    public class MainViewModel
    {
        [TestMethod]
        public void ModelTrainer_NullTest()
        {
            var model = new MLIDS.ModelTrainer.ViewModels.MainViewModel();
            
            Assert.IsNotNull(model.AvailableRunners);

            model.StartAction();
        }
    }
}