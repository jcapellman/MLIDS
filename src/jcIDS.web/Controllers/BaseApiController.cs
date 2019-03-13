using System;

using jcIDS.web.DAL;
using jcIDS.web.Managers;
using jcIDS.web.Objects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMemoryCache Cache;
        protected IDSContext DbContext;
        protected ConfigurationValues Configuration;

        public BaseApiController(IMemoryCache memoryCache, IDSContext dbContext, ConfigurationValues configuration)
        {
            Cache = memoryCache;
            DbContext = dbContext;
            Configuration = configuration;
        }

        protected int GetDeviceIdFromToken(string deviceToken)
        {
            var deviceId = new DeviceManager(Cache, DbContext).AuthenticateDevice(deviceToken);

            if (deviceId == null)
            {
                throw new UnauthorizedAccessException($"{deviceToken} is invalid");
            }

            return deviceId.Value;
        }
    }
}