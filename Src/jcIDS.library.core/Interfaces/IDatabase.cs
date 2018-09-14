using jcIDS.library.core.DAL.Objects.Base;

namespace jcIDS.library.core.Interfaces
{
    interface IDatabase
    {
        bool DeleteItem<T>(T item) where T: BaseObject;

        bool AddItem<T>(T item) where T : BaseObject;

        bool Contains<T>(T item) where T : BaseObject;

        T GetItem<T>(int ID) where T : BaseObject;
    }
}