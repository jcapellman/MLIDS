using jcIDS.library.core.DAL.Objects.Base;

namespace jcIDS.library.core.Interfaces
{
    interface IListManager<T> where T : BaseObject
    {
        bool IsContained(string resourceName);

        T GetItem(int id);

        bool AddItem(T item);
    }
}