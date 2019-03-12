using jcIDS.web.DAL;

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

        public BaseApiController(IMemoryCache memoryCache, IDSContext dbContext)
        {
            Cache = memoryCache;
            DbContext = dbContext;
        }
    }
}