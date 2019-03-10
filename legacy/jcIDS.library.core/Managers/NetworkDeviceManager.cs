using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using jcIDS.library.core.Common;
using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class NetworkDeviceManager : IListManager<NetworkDeviceObject>
    {
        public bool IsContained(Expression<Func<NetworkDeviceObject, bool>> expression) => CoreManager.GetService<IDatabase>().Contains(expression);

        public NetworkDeviceObject GetItem(Expression<Func<NetworkDeviceObject, bool>> expression) => CoreManager.GetService<IDatabase>().GetItem(expression);

        public int AddItem(NetworkDeviceObject item) => CoreManager.GetService<IDatabase>().AddItem(item);

        public List<NetworkDeviceObject> GetAll() => CoreManager.GetService<IDatabase>().GetAll<NetworkDeviceObject>();

        public string ToCSV() => string.Join(Environment.NewLine, GetAll().Select(a => a.ToCSV()));

        public bool UpdateItem(NetworkDeviceObject item) => CoreManager.GetService<IDatabase>().UpdateItem(null, item);
    }
}