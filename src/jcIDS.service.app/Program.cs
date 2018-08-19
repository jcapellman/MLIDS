using jcIDS.library.core.Managers;

namespace jcIDS.service.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var coreManager = new CoreManager();

            coreManager.Initialize();
        }
    }
}