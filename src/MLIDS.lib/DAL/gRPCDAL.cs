using Grpc.Net.Client;

using MLIDS.lib.Containers;
using MLIDS.lib.ML.Objects;
using MLIDS.Service.gRPC.Protos;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace MLIDS.lib.DAL
{
    public class gRPCDAL : Base.BaseDAL
    {
        private PacketStorage.PacketStorageClient _gClient;

        public override string Description => "gRPC";

        public override bool IsSelectable => true;

        public override async Task<List<PayloadItem>> GetHostPacketsAsync(string hostName)
        {
            throw new NotImplementedException();
        }

        public gRPCDAL(SettingsItem settings) : base(settings) { }

        public override bool Initialize()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress($"http://{settingsItem.DAL_HostIP}");

            _gClient = new PacketStorage.PacketStorageClient(channel);

            return true;
        }

        public override Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            var result = await _gClient.WritePacketAsync(new PacketStorageRequest
            {
               API = Common.Constants.API_VERSION,
               JSON = JsonSerializer.Serialize(packet)
            });

            return result.Success;
        }
    }
}