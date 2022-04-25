using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace MLIDS.UnitTests.lib.DAL
{
    [TestClass]
    public class CSVFileDAL
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CSVFileDAL_NullTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            await csvDal.GetHostPacketsAsync(null);
        }

        [TestMethod]
        public async Task CSVFileDAL_StringTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.GetHostPacketsAsync("test");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CSVFileDAL_WriteNullTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.WritePacketAsync(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CSVFileDAL_QueryNullTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.QueryPacketsAsync(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CSVFileDAL_QueryEmptyTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.QueryPacketsAsync(a => a.Label);

            Assert.IsTrue(!result.Any());
        }
    }
}
