using System.Collections.Generic;
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
            var tasks = new List<Task>();

            foreach (var packet in packets)
            {
                tasks.Add(_dbContext.Packets.AddAsync(new Packets(packet, deviceId)));
            }

            await Task.WhenAll(tasks);

            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}