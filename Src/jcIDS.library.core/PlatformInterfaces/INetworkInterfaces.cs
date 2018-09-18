using System.Threading.Tasks;

namespace jcIDS.library.core.PlatformInterfaces
{
    public interface INetworkInterfaces
    {
        bool IsOnline();

        string[] ScanDevices();
    }
}