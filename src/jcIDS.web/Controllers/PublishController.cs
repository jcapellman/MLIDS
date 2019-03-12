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
        public PublishController(IMemoryCache memoryCache) : base(memoryCache) { }

        private int GetDeviceIDFromToken(string deviceToken)
        {
            // TODO: Add actual logic
            return default;
        }

        [HttpPost]
        public async Task Post(PacketRequestItem requestItem)
        {
            var deviceID = GetDeviceIDFromToken(requestItem.DeviceToken);

            using (var ef = new EFEntities())
            {
                var tasks = new List<Task>();

                foreach (var packet in requestItem.Packets)
                {
                    tasks.Add(ef.Packets.AddAsync(new Packets(packet, deviceID)));
                }

                await Task.WhenAll(tasks);

                await ef.SaveChangesAsync();
            }
        }
    }
}