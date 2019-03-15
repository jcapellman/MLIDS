using System.Threading.Tasks;

using jcIDS.lib.CommonObjects;
using jcIDS.lib.RESTObjects;

namespace jcIDS.lib.Handlers
{
    public class PublishHandler : BaseHandler
    {
        public PublishHandler(string hostname) : base(hostname)
        {
        }

        protected override string EndPoint => "publish";

        /// <summary>
        /// Submits Packets to the service
        /// </summary>
        /// <param name="requestItem">Packets to be submitted</param>
        /// <returns>True if successfully submitted, False if in error</returns>
        public async Task<ReturnSet<bool>> SubmitPacketAsync(PacketRequestItem requestItem) =>
            await PostAsync<PacketRequestItem, bool>(EndPoint, requestItem);
    }
}