using System.Threading.Tasks;

using jcIDS.lib.RESTObjects;

namespace jcIDS.lib.Handlers
{
    public class PublishHandler : BaseHandler
    {
        public async Task<bool> SubmitPacketAsync(PacketRequestItem requestItem) =>
            await PostAsync<PacketRequestItem, bool>("publish", requestItem);
    }
}