using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using jcIDS.library.core.DAL.Objects.Base;

namespace jcIDS.library.core.Interfaces
{
    public interface IDatabase
    {
        bool DeleteItem<T>(T item) where T: BaseObject;

        int AddItem<T>(T item) where T : BaseObject;

        bool Contains<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        T GetItem<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        bool DeleteAll<T>() where T : BaseObject;

        bool UpdateItem<T>(Expression<Func<T, bool>> expression, T item) where T : BaseObject;

        List<T> GetAll<T>() where T : BaseObject;

        bool Initialize();
    }
}