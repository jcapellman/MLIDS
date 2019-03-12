using System;

using jcIDS.lib.Helpers;
using jcIDS.web.DAL;
using jcIDS.web.DAL.Tables;

namespace jcIDS.web.Managers
{
    public class DeviceManager
    {
        private readonly EFEntities _efEntities;

        public DeviceManager(EFEntities eFEntities)
        {
            _efEntities = eFEntities;
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

            _efEntities.Devices.Add(device);

            _efEntities.SaveChanges();

            return device;
        }
    }
}