using System;
using System.Linq.Expressions;

using jcIDS.library.core.DAL.Objects.Base;

namespace jcIDS.library.core.Interfaces
{
    interface IListManager<T> where T : BaseObject
    {
        bool IsContained(Expression<Func<T, bool>> expression);

        T GetItem(Expression<Func<T, bool>> expression);

        int AddItem(T item);
    }
}