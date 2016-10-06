using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

using jcIDS.Library.Common;

using jcIDS.Service.Library.Helpers;
using jcIDS.Service.Library.Objects;
using jcIDS.Service.Library.Scanners;

using Microsoft.Extensions.Configuration;

namespace jcIDS.Service.Library {
    public class MainService {
        private NetworkInterface currentNetworkInterface;

        private readonly Logger _logger;

        private List<BaseScanner> _enabledScanners;

        private readonly IConfigurationRoot _config;

        public MainService() {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            _logger = new Logger($"{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Year}_{DateTime.Now.Hour}_{DateTime.Now.Minute}.log");
        }

        private void writeLog(string message) {
            _logger.WriteMessage(message);
        }

        private void writeException(JCIDSException exception) {
            _logger.WriteMessage(exception);
        }

        private List<BaseScanner> getAvailableScanners() {
            var list = new List<BaseScanner>();

            var objs = typeof(BaseScanner).GetTypeInfo().Assembly.GetTypes();

            foreach (var obj in objs) {
                var typeInfo = obj.GetTypeInfo();

                if (typeInfo.IsAbstract || typeInfo.BaseType != typeof(BaseScanner)) {
                    continue;
                }

                list.Add((BaseScanner)Activator.CreateInstance(obj, SWrapperItem));
            }

            return list;
        }

        private ScannerWrapperItem SWrapperItem => new ScannerWrapperItem() {
            NetAdapter = currentNetworkInterface
        };

        public bool Initialize() {
            try {
                writeLog("Starting initialization of jcIDS");

                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                if (networkInterfaces == null) {
                    throw new JCIDSException("No Network Interfaces found");
                }

                foreach (var networkInterface in networkInterfaces) {
                    if (networkInterface.OperationalStatus != OperationalStatus.Up) {
                        continue;
                    }

                    currentNetworkInterface = networkInterface;

                    break;
                }

                if (currentNetworkInterface == null) {
                    throw new JCIDSException("Could not find a connected network");
                }

                writeLog($"Using {currentNetworkInterface.Description} Network Interface");

                _enabledScanners = new List<BaseScanner>();

                writeLog("Reading appSettings.json");

                var availableScanners = getAvailableScanners();

                writeLog($"{availableScanners.Count} available scanners");
                
                foreach (var item in _config.GetSection("Scanners").GetChildren()) {
                    if (item.Value.ToUpper() != "TRUE") {
                        continue;
                    }

                    var scanner = availableScanners.FirstOrDefault(a => a.Name() == item.Key);

                    if (scanner == null) {
                        continue;
                    }

                    writeLog($"Enabling {scanner.Name()} Scanner");

                    _enabledScanners.Add(scanner);
                }

                return true;
            } catch (JCIDSException exception) {
                writeException(exception);

                return false;
            }
        }

        public void Run() {
            while (true) {
                foreach (var scanner in _enabledScanners) {
                    scanner.Run();
                }
            }
        }
    }
}