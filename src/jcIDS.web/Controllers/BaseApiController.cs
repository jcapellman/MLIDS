using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace jcIDS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMemoryCache _cache;

        public BaseApiController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
    }
}