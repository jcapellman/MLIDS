using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class BlackListManager : IListManager<BlackListObject>
    {
        public bool IsContained(Expression<Func<BlackListObject, bool>> expression) => CoreManager.GetService<IDatabase>().Contains(expression);

        public BlackListObject GetItem(Expression<Func<BlackListObject, bool>> expression) => CoreManager.GetService<IDatabase>().GetItem(expression);

        public int AddItem(BlackListObject item) => CoreManager.GetService<IDatabase>().AddItem(item);

        public List<BlackListObject> GetAll() => CoreManager.GetService<IDatabase>().GetAll<BlackListObject>();
        public string ToCSV()
        {
            throw new NotImplementedException();
        }
    }
}