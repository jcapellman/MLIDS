using Windows.Networking.Connectivity;

using jcIDS.Service.UWP.Objects;

namespace jcIDS.Service.UWP.Scanners {
    public abstract class BaseScanner {
        protected NetworkAdapter NetworkAdapter;

        protected BaseScanner(ScannerWrapperItem scannerWrapper) {
            NetworkAdapter = scannerWrapper.NetAdapter;
        }

        public abstract void Run();
    }
}