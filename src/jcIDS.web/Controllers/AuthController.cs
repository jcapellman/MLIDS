using jcIDS.web.DAL;
using jcIDS.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    public class AuthController : BaseApiController
    {
        public AuthController(IMemoryCache memoryCache, IDSContext dbContext) : base(memoryCache, dbContext)
        {
        }

        [HttpPost]
        public string Post(string deviceName)
        {
            var device = new DeviceManager(Cache, DbContext).RegisterDevice(deviceName);

            return device.Token;
        }
    }
}