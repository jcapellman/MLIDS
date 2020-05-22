using Microsoft.VisualStudio.TestTools.UnitTesting;

using MLIDS.lib.DAL;

using System;
using System.IO;
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

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public async Task Trainer_ModelNotFoundTest()
        {
            await new MLIDS.lib.ML.Trainer().GenerateModel(new MongoDAL(new MLIDS.lib.Containers.SettingsItem()), "test");
        }
    }
}