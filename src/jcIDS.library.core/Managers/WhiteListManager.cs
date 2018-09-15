using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class WhiteListManager : IListManager<WhiteListObject>
    {
        public bool IsContained(string resourceName) => CoreManager.GetService<IDatabase>().Contains(new WhiteListObject
        {
            ResourceName = resourceName
        });

        public WhiteListObject GetItem(int id) => CoreManager.GetService<IDatabase>().GetItem<WhiteListObject>(id);

        public bool AddItem(WhiteListObject item) => CoreManager.GetService<IDatabase>().AddItem(item) != default(int);
    }
}