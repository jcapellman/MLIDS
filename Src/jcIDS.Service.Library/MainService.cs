using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using jcIDS.Library.Common;

using jcIDS.Service.Library.Helpers;
using jcIDS.Service.Library.Scanners;

namespace jcIDS.Service.Library {
    public class MainService {
        private NetworkInterface currentNetworkInterface;

        private readonly Logger _logger;

        private List<BaseScanner> _enabledScanners;

        public MainService() {
            _logger = new Logger($"{DateTime.Now}.log");
        }

        public bool Initialize() {
            try {
                _enabledScanners = new List<BaseScanner>();

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

                return true;
            } catch (JCIDSException exception) {
                _logger.WriteMessage(exception);

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