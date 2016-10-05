using jcIDS.Service.Library.Objects;

namespace jcIDS.Service.Library.Scanners {
    public class NetworkScanner : BaseScanner {
        public NetworkScanner(ScannerWrapperItem scannerWrapper) : base(scannerWrapper) { }

        public override void Run() {
            
        }

        public override string Name() => "Network";
    }
}