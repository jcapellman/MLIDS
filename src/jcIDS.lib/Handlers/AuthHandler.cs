using System.Threading.Tasks;

using jcIDS.lib.CommonObjects;

namespace jcIDS.lib.Handlers
{
    public class AuthHandler : BaseHandler
    {
        public AuthHandler(string hostname) : base(hostname)
        {
        }

        protected override string EndPoint => "auth";

        /// <summary>
        /// Posts the DeviceName to be Registered with the Service
        /// </summary>
        /// <param name="deviceName">Device Name of the current machine</param>
        /// <returns>Token of the Device or Exception during the attempt</returns>
        public async Task<ReturnSet<string>> RegisterDeviceAsync(string deviceName) =>
            await PostAsync<string, string>(EndPoint, deviceName);
    }
}