using System.Collections.Generic;
using System.Threading.Tasks;

using jcIDS.lib.RESTObjects;

using jcIDS.web.DAL;
using jcIDS.web.DAL.Tables;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    public class PublishController : BaseApiController
    {
        public PublishController(IMemoryCache memoryCache, IDSContext dbContext) : base(memoryCache, dbContext) { }

        [HttpPost]
        public async Task Post(PacketRequestItem requestItem)
        {
            var deviceID = GetDeviceIdFromToken(requestItem.DeviceToken);

            var tasks = new List<Task>();

            foreach (var packet in requestItem.Packets)
            {
                tasks.Add(DbContext.Packets.AddAsync(new Packets(packet, deviceID)));
            }

            await Task.WhenAll(tasks);

            await DbContext.SaveChangesAsync();
        }
    }
}