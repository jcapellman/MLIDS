using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class BlackListManager : IListManager<BlackListObject>
    {
        public bool IsContained(string resourceName) => CoreManager.GetService<IDatabase>().Contains(new BlackListObject
        {
            ResourceName = resourceName
        });

        public BlackListObject GetItem(int id) => CoreManager.GetService<IDatabase>().GetItem<BlackListObject>(id);

        public bool AddItem(BlackListObject item) => CoreManager.GetService<IDatabase>().AddItem(item) != default(int);
    }
}