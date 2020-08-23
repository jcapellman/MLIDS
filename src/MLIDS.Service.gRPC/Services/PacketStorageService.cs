using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using MLIDS.Service.gRPC.Protos;

namespace MLIDS.Service.gRPC.Services
{
    public class PacketStorageService : PacketStorage.PacketStorageBase
    {
        private readonly ILogger<PacketStorageService> _logger;

        public PacketStorageService(ILogger<PacketStorageService> logger)
        {
            _logger = logger;
        }

        public override Task<PacketStorageReply> WritePacket(PacketStorageRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PacketStorageReply
            {
                Success = true
            });
        }
    }
}