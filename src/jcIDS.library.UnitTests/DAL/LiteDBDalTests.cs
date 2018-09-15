using jcIDS.library.core.DAL;
using jcIDS.library.core.DAL.Objects.Base;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.DAL
{
    [TestClass]
    public class LiteDBDalTests
    {
        private class TestObject : BaseObject
        {

        }

        [TestMethod]
        public void GetNullItem()
        {
            var litedb = new LiteDBDAL();

            var val = litedb.GetItem<TestObject>(a => a.ID == 0);

            Assert.IsNull(val);
        }

        [TestMethod]
        public void ContainsNullItem()
        {
            var litedb = new LiteDBDAL();

            var contains = litedb.Contains<TestObject>(a => a == null);

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var litedb = new LiteDBDAL();

            var contains = litedb.Contains<TestObject>(a => a.ID == 0);

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsHit()
        {
            var litedb = new LiteDBDAL();

            var resultID = litedb.AddItem(new TestObject());

            Assert.IsTrue(resultID > 0);

            var contains = litedb.Contains<TestObject>(a => a.ID == resultID);

            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void DeleteNull()
        {
            var litedb = new LiteDBDAL();

            var result = litedb.DeleteItem<TestObject>(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteMiss()
        {
            var litedb = new LiteDBDAL();

            var result = litedb.DeleteItem(new TestObject());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteHit()
        {
            var litedb = new LiteDBDAL();

            var deleteResult = litedb.DeleteAll<TestObject>();

            Assert.IsTrue(deleteResult);

            var resultID = litedb.AddItem(new TestObject());

            var item = litedb.GetItem<TestObject>(a => a.ID == resultID);

            Assert.IsNotNull(item);

            var result = litedb.DeleteItem(item);

            Assert.IsTrue(result);
        }
    }
}