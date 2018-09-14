using jcIDS.library.core.DAL;
using jcIDS.library.core.DAL.Objects.Base;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.DAL
{
    [TestClass]
    public class LiteDBDalTests
    {
        public class TestObject : BaseObject
        {

        }

        [TestMethod]
        public void GetNullItem()
        {
            var litedb = new LiteDBDAL();

            var val = litedb.GetItem<TestObject>(0);

            Assert.IsNull(val);
        }
    }
}