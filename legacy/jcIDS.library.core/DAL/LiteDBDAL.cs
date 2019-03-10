using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

using jcIDS.library.core.Common;
using jcIDS.library.core.DAL.Objects.Base;
using jcIDS.library.core.Interfaces;
using jcIDS.library.core.Managers;

namespace jcIDS.library.core.DAL
{
    public class LiteDBDAL : IDatabase
    {
        public int AddItem<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
            {
                var collection = db.GetCollection<T>();

                return collection.Insert(item);
            }
        }

        public bool Contains<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
            {
                return db.GetCollection<T>().FindOne(expression) != null;
            }
        }

        public T GetItem<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
            {
                var collection = db.GetCollection<T>();

                return collection.FindOne(expression);
            }
        }

        public bool DeleteAll<T>() where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
            {
                return db.DropCollection(typeof(T).Name);
            }
        }

        public bool UpdateItem<T>(Expression<Func<T, bool>> expression, T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
            {
                return db.GetCollection<T>().Update(item);
            }
        }

        public List<T> GetAll<T>() where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
            {
                return new List<T>(db.GetCollection<T>().FindAll());
            }
        }

        public bool Initialize()
        {
            if (!File.Exists(CoreManager.GetService<Config>().DBFileName))
            {
                throw new FileNotFoundException("Could not find LiteDB File", CoreManager.GetService<Config>().DBFileName);
            }

            return true;
        }

        public bool DeleteItem<T>(T item) where T : BaseObject
        {
            using (var db = new LiteDB.LiteDatabase(CoreManager.GetService<Config>().DBFileName))
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