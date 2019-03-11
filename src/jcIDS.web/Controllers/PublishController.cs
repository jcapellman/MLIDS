using System.Collections.Generic;

using jcIDS.lib.Objects;

using Microsoft.AspNetCore.Mvc;

namespace jcIDS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        [HttpPost]
        public void Post(List<PacketArrivedEventArgs> packet)
        {

        }
    }
}