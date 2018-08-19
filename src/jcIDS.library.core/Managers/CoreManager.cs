using System;

using jcIDS.library.core.Common;

using Microsoft.Extensions.DependencyInjection;

namespace jcIDS.library.core.Managers
{
    public class CoreManager
    {
        private ServiceProvider Container;

        private BlackListManager _blackListManager;

        public void Initialize()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(_blackListManager);

            Container = serviceCollection.BuildServiceProvider();

            Console.WriteLine(Constants.APP_NAME);

            Container.GetService<BlackListManager>();
        }
    }
}