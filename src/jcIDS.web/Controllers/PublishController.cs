using System.Threading.Tasks;

using jcIDS.lib.RESTObjects;

using jcIDS.web.DAL;
using jcIDS.web.Managers;
using jcIDS.web.Objects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    public class PublishController : BaseApiController
    {
        public PublishController(IMemoryCache memoryCache, IDSContext dbContext, ConfigurationValues configuration) : base(memoryCache, dbContext, configuration) { }

        [HttpPost]
        public async Task<bool> Post(PacketRequestItem requestItem)
        {
            var deviceId = GetDeviceIdFromToken(requestItem.DeviceToken);

            return await new PacketManager(DbContext).StorePacketsAsync(requestItem.Packets, deviceId);
        }
    }
}