using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;

namespace MLIDS.UnitTests.lib.Windows.ViewModels
{
    [TestClass]
    public class BaseViewModel
    {
        public class TestViewModel : MLIDS.lib.Windows.ViewModels.BaseViewModel
        {
            public override void StartAction()
            {
                
            }

            public override void StartButtonEnablement()
            {
                
            }

            public override void StopAction()
            {
                
            }

            public TestViewModel()
            {

            }
        }

        [TestMethod]
        [ExpectedException(typeof(DllNotFoundException))]
        public void BaseViewModel_EmptyTest()
        {
            var viewModel = new TestViewModel();

            Assert.IsTrue(viewModel.DataLayers.Any());
            Assert.IsNotNull(viewModel.SelectedDataLayer);

            var selectedLayer = viewModel.SelectedDataLayer;

            Assert.IsNotNull(selectedLayer);
        }
    }
}