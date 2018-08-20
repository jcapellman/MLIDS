using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class WhiteListManager : IListManager
    {
        public bool IsContained(string resourceItem)
        {
            return false;
        }
    }
}