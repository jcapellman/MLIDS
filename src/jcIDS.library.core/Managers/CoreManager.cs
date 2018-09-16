using jcIDS.library.core.DAL;
using jcIDS.library.core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using NLog;

namespace jcIDS.library.core.Managers
{
    public class CoreManager
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        private static ServiceProvider _container;
        
        private readonly LicenseManager _licenseManager = new LicenseManager();

        public static T GetService<T>() => _container.GetService<T>();

        public bool Initialize()
        {
            if (!_licenseManager.IsRegistered())
            {
                _log.Debug("Not registered - shutting down");

                return false;
            }
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient(typeof(IDatabase), typeof(LiteDBDAL));

            serviceCollection.AddSingleton(new BlackListManager());
            serviceCollection.AddSingleton(new WhiteListManager());
            serviceCollection.AddSingleton(new NetworkDeviceManager());

            _container = serviceCollection.BuildServiceProvider();

            return true;
        }

        public void StartService()
        {
            _log.Debug("Starting service");
        }

        public void StopService()
        {
            _container.Dispose();

            _log.Debug("Shutting down service");
        }
    }
}