using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class BlackListManager : IListManager
    {
        public bool IsContained(string resourceItem)
        {
            return CoreManager.GetService<IDatabase>().Contains<BlackListObject>(new BlackListObject());
        }
    }
}