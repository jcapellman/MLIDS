using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.Detector
{
    [TestClass]
    public class MainViewModel
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DetectorMainViewModel_NullTest()
        {
            var mainViewModel = new MLIDS.Detector.ViewModels.MainViewModel();

            mainViewModel.PacketProcessing(null);
        }
    }
}