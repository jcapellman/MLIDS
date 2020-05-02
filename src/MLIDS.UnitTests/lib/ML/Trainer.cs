using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading.Tasks;

namespace MLIDS.UnitTests.lib.ML
{
    [TestClass]
    public class Trainer
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Trainer_NullTest()
        {
            await new MLIDS.lib.ML.Trainer().GenerateModel(null, null);
        }
    }
}