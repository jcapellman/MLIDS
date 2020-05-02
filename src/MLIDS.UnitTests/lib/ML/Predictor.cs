using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.lib.ML
{
    [TestClass]
    public class Predictor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Predictor_NullTest()
        {
            new MLIDS.lib.ML.Predictor(null);
        }
    }
}