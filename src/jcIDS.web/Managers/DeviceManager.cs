using System;
using System.Linq;

using jcIDS.lib.Helpers;
using jcIDS.web.DAL;
using jcIDS.web.DAL.Tables;

using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Managers
{
    public class DeviceManager
    {
        private readonly IDSContext _idsContext;
        private readonly IMemoryCache _memoryCache;

        public DeviceManager(IMemoryCache memoryCache, IDSContext eFEntities)
        {
            _memoryCache = memoryCache;
            _idsContext = eFEntities;
        }

        public int? AuthenticateDevice(string token)
        {
            if (_memoryCache.TryGetValue(token, out int id))
            {
                return id;
            }

            var device = _idsContext.Devices.FirstOrDefault(a => a.Token == token && a.Active);

            if (device == null)
            {
                return null;
            }

            _memoryCache.Set(token, device.ID);

            return device.ID;
        }

        public Devices RegisterDevice(string deviceName)
        {
            var device = new Devices
            {
                Active = true,
                Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now,
                Name = deviceName,
                Token = deviceName.SHA1()
            };

            _idsContext.Devices.Add(device);

            _idsContext.SaveChanges();

            _memoryCache.Set(device.Token, device.ID);

            return device;
        }
    }
}