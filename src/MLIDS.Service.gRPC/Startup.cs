using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL;
using MLIDS.lib.DAL.Base;

using MLIDS.Service.gRPC.Services;

namespace MLIDS.Service.gRPC
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = SettingsItem.Load();

            // Swap out the Class if DAL swap
            var dal = new MongoDAL(settings);

            dal.Initialize();

            services.AddSingleton<BaseDAL>(dal);

            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<PacketStorageService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}