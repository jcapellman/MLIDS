using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class WhiteListManager : IListManager<WhiteListObject>
    {
        public bool IsContained(Expression<Func<WhiteListObject, bool>> expression) => CoreManager.GetService<IDatabase>().Contains(expression);

        public WhiteListObject GetItem(Expression<Func<WhiteListObject, bool>> expression) => CoreManager.GetService<IDatabase>().GetItem(expression);

        public int AddItem(WhiteListObject item) => CoreManager.GetService<IDatabase>().AddItem(item);

        public List<WhiteListObject> GetAll() => CoreManager.GetService<IDatabase>().GetAll<WhiteListObject>();
        public string ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}