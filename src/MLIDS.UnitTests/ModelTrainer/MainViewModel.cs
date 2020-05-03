using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.ModelTrainer
{
    [TestClass]
    public class MainViewModel
    {
        [TestMethod]
        [ExpectedException(typeof(NullObjectException))]
        public void ModelTrainer_NullTest()
        {
            var model = new MLIDS.ModelTrainer.ViewModels.MainViewModel();

            model.StartAction();

            model.StopAction();
        }
    }
}
