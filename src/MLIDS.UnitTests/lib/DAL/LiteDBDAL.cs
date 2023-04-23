using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.lib.DAL
{
    [TestClass]
    public class LiteDBDAL
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LiteDBDAL_NullTest()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_StringTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(new MLIDS.lib.Containers.SettingsItem());

            await litedb.GetHostPacketsAsync("test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_WriteNullTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(new MLIDS.lib.Containers.SettingsItem());

            await litedb.WritePacketAsync(null);
        }

        [TestMethod]
        public async Task LiteDBDAL_WriteEmptyTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(new MLIDS.lib.Containers.SettingsItem
            {
                DAL_FileName = "testo.db"
            });

            var result = litedb.Initialize();

            Assert.IsTrue(result);

            result = await litedb.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem());

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_QueryNullTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(new MLIDS.lib.Containers.SettingsItem());

            await litedb.QueryPacketsAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_QueryEmptyTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await litedb.QueryPacketsAsync(a => a.Label);

            Assert.IsTrue(!result.Any());
        }

        [TestMethod]
        public void LiteDBDAL_Initialize()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = litedb.Initialize();

            Assert.IsTrue(result);
        }
    }
}