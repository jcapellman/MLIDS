using System;
using System.Linq.Expressions;

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

        public bool Contains<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                return db.GetCollection<T>().FindOne(expression) != null;
            }
        }

        public T GetItem<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                var collection = db.GetCollection<T>();

                return collection.FindOne(expression);
            }
        }

        public bool DeleteAll<T>() where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                return db.DropCollection(typeof(T).Name);
            }
        }

        public bool UpdateItem<T>(Expression<Func<T, bool>> expression, T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(FILENAME))
            {
                return db.GetCollection<T>().Update(item);
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
    }
}