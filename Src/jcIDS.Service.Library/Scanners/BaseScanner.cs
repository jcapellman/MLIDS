using System.Net.NetworkInformation;

using jcIDS.Service.Library.Objects;

namespace jcIDS.Service.Library.Scanners {
    public abstract class BaseScanner {
        protected NetworkInterface NetworkAdapter;

        protected BaseScanner(ScannerWrapperItem scannerWrapper) {
            NetworkAdapter = scannerWrapper.NetAdapter;
        }

        public abstract void Run();
    }
}