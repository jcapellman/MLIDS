using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

using jcIDS.Library.Common;
using jcIDS.Service.Helpers;
using jcIDS.Service.Scanners;

namespace jcIDS.Service {
    public class MainService {
        private NetworkInterface currentNetworkInterface;

        private readonly Logger _logger;

        private List<BaseScanner> _enabledScanners;

        public MainService() {
            _logger = new Logger($"{DateTime.Now}.log");
        }

        public bool Initialize() {
            try {
                currentNetworkInterface =
                    NetworkInterface.GetAllNetworkInterfaces()
                        .FirstOrDefault(a => a.OperationalStatus == OperationalStatus.Up);

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