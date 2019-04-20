using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using jcIDS.lib.CommonObjects;
using jcIDS.web.DAL;
using jcIDS.web.DAL.Tables;

namespace jcIDS.web.Managers
{
    public class PacketManager
    {
        private readonly IDSContext _dbContext;

        public PacketManager(IDSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> StorePacketsAsync(List<Packet> packets, int deviceId)
        {
            await _dbContext.Packets.AddRangeAsync(packets.Select(a => new Packets(a, deviceId)));

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}