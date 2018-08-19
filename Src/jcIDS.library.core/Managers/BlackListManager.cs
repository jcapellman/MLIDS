using System;
using System.Collections.Generic;
using System.Text;
using jcIDS.library.core.Interfaces;

namespace jcIDS.library.core.Managers
{
    public class BlackListManager : IListManager
    {
        public bool IsContained(string resourceItem)
        {
            return false;
        }
    }
}