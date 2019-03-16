using jcIDS.web.DAL;
using jcIDS.web.Managers;
using jcIDS.web.Objects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    public class PredictController : BaseApiController
    {
        public PredictController(IMemoryCache memoryCache, IDSContext dbContext, ConfigurationValues configuration) : base(memoryCache, dbContext, configuration)
        {
        }

        [HttpGet]
        public byte[] GetModel(int deviceId)
        {
            var result = new DeviceManager(Cache, DbContext).GetDeviceModel(deviceId);

            if (!result.HasObjectError)
            {
                return result.ObjectValue;
            }

            Log.Error(result.ObjectException, result.ObjectExceptionInformation);

            return null;

        }
    }
}