using System;
using System.Linq.Expressions;

using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class NetworkDeviceManager : IListManager<NetworkDeviceObject>
    {
        public bool IsContained(Expression<Func<NetworkDeviceObject, bool>> expression) => CoreManager.GetService<IDatabase>().Contains(expression);

        public NetworkDeviceObject GetItem(Expression<Func<NetworkDeviceObject, bool>> expression) => CoreManager.GetService<IDatabase>().GetItem(expression);

        public int AddItem(NetworkDeviceObject item) => CoreManager.GetService<IDatabase>().AddItem(item);
    }
}