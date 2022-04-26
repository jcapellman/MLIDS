using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.DataCapture
{
    [TestClass]
    public class MainViewModel
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Ignore]
        public void DataCapture_NullTest()
        {
            var model = new MLIDS.DataCapture.ViewModels.MainViewModel();

            model.PacketProcessing(null);
        }
    }
}