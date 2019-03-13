using System.Threading.Tasks;

namespace jcIDS.lib.Handlers
{
    public class AuthHandler : BaseHandler
    {
        public async Task<string> RegisterDeviceAsync(string deviceName) =>
            await PostAsync<string, string>("auth", deviceName);
    }
}