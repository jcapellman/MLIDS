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

        [TestMethod]
        public void ContainsNullItem()
        {
            var litedb = new LiteDBDAL();

            var contains = litedb.Contains<TestObject>(null);

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var litedb = new LiteDBDAL();

            var contains = litedb.Contains<TestObject>(new TestObject { ID = 0 });

            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsHit()
        {
            var litedb = new LiteDBDAL();

            var result = litedb.AddItem(new TestObject());

            Assert.IsTrue(result);

            var contains = litedb.Contains(new TestObject { ID = 1 });

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

            var result = litedb.DeleteItem<TestObject>(new TestObject());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteHit()
        {
            var litedb = new LiteDBDAL();

            litedb.AddItem(new TestObject());

            var item = litedb.GetItem<TestObject>(1);

            Assert.IsNotNull(item);

            var result = litedb.DeleteItem(item);

            Assert.IsFalse(result);
        }
    }
}