using System;
using System.Collections.Generic;

using Windows.Networking.Connectivity;

using jcIDS.Library.Common;

using jcIDS.Service.UWP.Helpers;
using jcIDS.Service.UWP.Scanners;

namespace jcIDS.Service.UWP {
    public class MainService {
        private NetworkAdapter currentNetworkInterface;

        private readonly Logger _logger;

        private List<BaseScanner> _enabledScanners;

        public MainService() {
            _logger = new Logger($"{DateTime.Now}.log");
        }

        public bool Initialize() {
            try {
                _enabledScanners = new List<BaseScanner>();

                var networkInterfaces = Windows.Networking.Connectivity.NetworkInformation.GetConnectionProfiles();

                if (networkInterfaces == null) {
                    throw new JCIDSException("No Network Interfaces found");
                }

                foreach (var networkInterface in networkInterfaces) {
                    var status = networkInterface.GetNetworkConnectivityLevel();

                    if (status == NetworkConnectivityLevel.None) {
                        continue;
                    }

                    currentNetworkInterface = networkInterface.NetworkAdapter;

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
            foreach (var scanner in _enabledScanners) {
                scanner.Run();
            }
        }
    }
}