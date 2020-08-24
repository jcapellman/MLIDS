using System.Text.Json;
using System.Threading.Tasks;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using MLIDS.lib.DAL;
using MLIDS.lib.ML.Objects;

using MLIDS.Service.gRPC.Protos;

namespace MLIDS.Service.gRPC.Services
{
    public class PacketStorageService : PacketStorage.PacketStorageBase
    {
        private readonly ILogger<PacketStorageService> _logger;
        private MongoDAL _dbService = new MongoDAL(new lib.Containers.SettingsItem());

        public PacketStorageService(ILogger<PacketStorageService> logger)
        {
            _logger = logger;

            _dbService.Initialize();
        }

        public override async Task<PacketStorageReply> WritePacket(PacketStorageRequest request, ServerCallContext context)
        {
            var result = await _dbService.WritePacketAsync(JsonSerializer.Deserialize<PayloadItem>(request.JSON));

            return new PacketStorageReply
            {
                Success = result
            };
        }
    }
}