using System.Text.Json;
using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

using MLIDS.Service.gRPC.Protos;

namespace MLIDS.Service.gRPC.Services
{
    public class PacketStorageService : PacketStorage.PacketStorageBase
    {
        private readonly ILogger<PacketStorageService> _logger;
        private BaseDAL _dal;
        
        public PacketStorageService(ILogger<PacketStorageService> logger, BaseDAL dal)
        {
            _logger = logger;

            _dal = dal;
        }

        public override async Task<PacketStorageReply> WritePacket(PacketStorageRequest request, ServerCallContext context)
        {
            var result = await _dal.WritePacketAsync(JsonSerializer.Deserialize<PayloadItem>(request.JSON));

            return new PacketStorageReply
            {
                Success = result
            };
        }
    }
}