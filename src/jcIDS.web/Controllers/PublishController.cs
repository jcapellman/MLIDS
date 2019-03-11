using System.Collections.Generic;
using System.Threading.Tasks;

using jcIDS.lib.RESTObjects;
using jcIDS.web.DAL;
using jcIDS.web.DAL.Tables;

using Microsoft.AspNetCore.Mvc;

namespace jcIDS.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        [HttpPost]
        public async Task Post(PacketRequestItem requestItem)
        {
            using (var ef = new EFEntities())
            {
                var tasks = new List<Task>();

                foreach (var packet in requestItem.Packets)
                {
                    tasks.Add(ef.Packets.AddAsync(new Packets(packet)));
                }

                await Task.WhenAll(tasks);

                await ef.SaveChangesAsync();
            }
        }
    }
}