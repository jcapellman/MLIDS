using System.Net.NetworkInformation;

namespace jcIDS.Service.Scanners {
    public abstract class BaseScanner {
        protected NetworkInterface NetworkInterface;

        protected BaseScanner(NetworkInterface networkInterface) {
            NetworkInterface = networkInterface;
        }

        public abstract void Run();
    }
}