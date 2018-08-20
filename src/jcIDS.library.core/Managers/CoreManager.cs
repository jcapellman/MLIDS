using Microsoft.Extensions.DependencyInjection;

using NLog;

namespace jcIDS.library.core.Managers
{
    public class CoreManager
    {
        private readonly Logger log = LogManager.GetCurrentClassLogger();

        private static ServiceProvider _container;

        private BlackListManager _blackListManager;

        private WhiteListManager _whiteListManager;

        private readonly LicenseManager _licenseManager = new LicenseManager();

        public static T GetService<T>() => _container.GetService<T>();

        public bool Initialize()
        {
            if (!_licenseManager.IsRegistered())
            {
                log.Debug("Not registered - shutting down");

                return false;
            }

            _blackListManager = new BlackListManager();
            _whiteListManager = new WhiteListManager();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(_blackListManager);
            serviceCollection.AddSingleton(_whiteListManager);

            _container = serviceCollection.BuildServiceProvider();

            return true;
        }

        public void StartService()
        {

        }

        public void StopService()
        {

        }
    }
}