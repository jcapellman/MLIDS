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
        public async Task LiteDBDAL_NullTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL();

            await litedb.GetHostPacketsAsync(null);
        }

        [TestMethod]
        public async Task LiteDBDAL_StringTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL();

            await litedb.GetHostPacketsAsync("test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_WriteNullTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL();

            await litedb.WritePacketAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_QueryNullTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL();

            await litedb.QueryPacketsAsync(null);
        }

        [TestMethod]
        public async Task LiteDBDAL_QueryEmptyTestAsync()
        {
            var litedb = new MLIDS.lib.DAL.LiteDBDAL();

            var result = await litedb.QueryPacketsAsync(a => a.Label);

            Assert.IsTrue(!result.Any());
        }
    }
}