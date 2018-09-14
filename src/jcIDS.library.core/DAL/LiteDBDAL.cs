using jcIDS.library.core.DAL.Objects.Base;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.DAL
{
    public class LiteDBDAL : IDatabase
    {
        private const string FILENAME = "main.db";

        public int AddItem<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                var collection = db.GetCollection<T>();

                return collection.Insert(item);
            }
        }

        public bool Contains<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                if (item == null)
                {
                    return false;
                }
                
                return db.GetCollection<T>().FindOne(a => a.ID == item.ID) != null;
            }
        }

        public bool DeleteAll<T>() where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                return db.DropCollection(typeof(T).Name);
            }
        }

        public bool DeleteItem<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                if (item == null)
                {
                    return false;
                }

                var collection = db.GetCollection<T>();

                return collection.Delete(item.ID);
            }
        }

        public T GetItem<T>(int ID) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                var collection = db.GetCollection<T>();

                return collection.FindOne(a => a.ID == ID);
            }
        }
    }
}