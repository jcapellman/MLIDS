using System.Net.NetworkInformation;

namespace jcIDS.Service.Scanners {
    public class NetworkScanner : BaseScanner {
        public NetworkScanner(NetworkInterface networkInterface) : base(networkInterface) { }

        public override void Run() {
            
        }
    }
}