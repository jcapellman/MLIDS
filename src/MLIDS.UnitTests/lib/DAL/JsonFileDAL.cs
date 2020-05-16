using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.lib.DAL
{
    [TestClass]
    public class JsonDAL
    {
        [TestMethod]
        public async Task LiteDBDAL_NullTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL();

            await jsonDal.GetHostPacketsAsync(null);
        }

        [TestMethod]
        public async Task LiteDBDAL_StringTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL();

            await jsonDal.GetHostPacketsAsync("test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_WriteNullTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL();

            await jsonDal.WritePacketAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task LiteDBDAL_QueryNullTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL();

            await jsonDal.QueryPacketsAsync(null);
        }

        [TestMethod]
        public async Task LiteDBDAL_QueryEmptyTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL();

            var result = await jsonDal.QueryPacketsAsync(a => a.Label);

            Assert.IsTrue(!result.Any());
        }
    }
}