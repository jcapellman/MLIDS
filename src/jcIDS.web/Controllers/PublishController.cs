using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using jcIDS.lib.RESTObjects;

using jcIDS.web.DAL;
using jcIDS.web.DAL.Tables;
using jcIDS.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    public class PublishController : BaseApiController
    {
        public PublishController(IMemoryCache memoryCache, IDSContext dbContext) : base(memoryCache, dbContext) { }

        private int GetDeviceIdFromToken(string deviceToken)
        {
            var deviceId = new DeviceManager(_cache, _dbContext).AuthenticateDevice(deviceToken);

            if (deviceId == null)
            {
                throw new UnauthorizedAccessException($"{deviceToken} is invalid");
            }

            return deviceId.Value;
        }

        [HttpPost]
        public async Task Post(PacketRequestItem requestItem)
        {
            var deviceID = GetDeviceIdFromToken(requestItem.DeviceToken);

            var tasks = new List<Task>();

            foreach (var packet in requestItem.Packets)
            {
                tasks.Add(_dbContext.Packets.AddAsync(new Packets(packet, deviceID)));
            }

            await Task.WhenAll(tasks);

            await _dbContext.SaveChangesAsync();
        }
    }
}