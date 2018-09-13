using jcIDS.library.core.DAL.Objects.Base;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.DAL
{
    public class LiteDBDAL : IDatabase
    {
        private const string FILENAME = "main.db";

        public bool AddItem<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                var collection = db.GetCollection<T>();

                return collection.Insert(item) > 0;
            }
        }

        public bool Contains<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                return db.GetCollection<T>().FindOne(a => a.ID == item.ID) != null;
            }
        }

        public bool DeleteItem<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                var collection = db.GetCollection<T>();

                return collection.Delete(item.ID);
            }
        }
    }
}