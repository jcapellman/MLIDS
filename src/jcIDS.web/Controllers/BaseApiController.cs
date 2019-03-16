using System;

using jcIDS.web.DAL;
using jcIDS.web.Managers;
using jcIDS.web.Objects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using NLog;

namespace jcIDS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMemoryCache Cache;
        protected IDSContext DbContext;
        protected ConfigurationValues Configuration;

        protected NLog.Logger Log = LogManager.GetCurrentClassLogger();

        public BaseApiController(IMemoryCache memoryCache, IDSContext dbContext, ConfigurationValues configuration)
        {
            Cache = memoryCache;
            DbContext = dbContext;
            Configuration = configuration;
        }

        protected int GetDeviceIdFromToken(string deviceToken)
        {
            var deviceIdResult = new DeviceManager(Cache, DbContext).AuthenticateDevice(deviceToken);

            if (deviceIdResult.HasObjectError || !deviceIdResult.ObjectValue.HasValue)
            {
                throw new UnauthorizedAccessException($"{deviceToken} is invalid");
            }

            return deviceIdResult.ObjectValue.Value;
        }
    }
}