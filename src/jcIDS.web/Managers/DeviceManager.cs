using System;
using System.Linq;

using jcIDS.lib.CommonObjects;
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

        public ReturnSet<int?> AuthenticateDevice(string token)
        {
            try
            {
                if (_memoryCache.TryGetValue(token, out int id))
                {
                    return new ReturnSet<int?>(id);
                }

                var device = _idsContext.Devices.FirstOrDefault(a => a.Token == token && a.Active);

                if (device == null)
                {
                    return new ReturnSet<int?>(null);
                }

                _memoryCache.Set(token, device.ID);

                return new ReturnSet<int?>(device.ID);
            }
            catch (Exception ex)
            {
                return new ReturnSet<int?>(ex, $"Auth failed using {token}");
            }
        }

        public ReturnSet<Devices> RegisterDevice(string deviceName)
        {
            try
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

                return new ReturnSet<Devices>(device);
            }
            catch (Exception ex)
            {
                return new ReturnSet<Devices>(ex, $"Failed to register device ({deviceName})");
            }
        }
    }
}