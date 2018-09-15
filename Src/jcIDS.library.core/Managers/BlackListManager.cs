using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Interfaces;

using System;

namespace jcIDS.library.core.Managers
{
    public class BlackListManager : IListManager
    {
        public bool IsContained(string resourceItem)
        {
            if (string.IsNullOrEmpty(resourceItem))
            {
                throw new ArgumentNullException(resourceItem);
            }

            return CoreManager.GetService<IDatabase>().Contains<BlackListObject>(new BlackListObject());
        }
    }
}