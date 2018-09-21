﻿using System;
using System.Threading;

using jcIDS.library.core.DAL;
using jcIDS.library.core.Interfaces;
using jcIDS.library.core.PlatformInterfaces;

using Microsoft.Extensions.DependencyInjection;

using NLog;

using NetworkInterface = jcIDS.library.core.PlatformImplementations.NetworkInterface;

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

            serviceCollection.AddTransient(typeof(INetworkInterfaces), typeof(NetworkInterface));

            _container = serviceCollection.BuildServiceProvider();

            return true;
        }

        public void StartService()
        {
            _log.Debug("Starting service");

            while (true)
            {
                var devices = _container.GetService<INetworkInterfaces>().ScanDevices();

                foreach (var device in devices)
                {
                    var item = _container.GetService<NetworkDeviceManager>().GetItem(a => a.MAC == device.MAC);

                    if (item == null)
                    {
                        _container.GetService<NetworkDeviceManager>().AddItem(device);
                    }
                    else
                    {
                        item.LastOnline = DateTime.Now;
                        
                        _container.GetService<NetworkDeviceManager>().UpdateItem(item);
                    }
                }

                Thread.Sleep(1000 * 60);
            }
        }

        public void StopService()
        {
            _container.Dispose();

            _log.Debug("Shutting down service");
        }
    }
}