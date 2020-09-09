using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL;
using MLIDS.lib.DAL.Base;

using MLIDS.Web.Blazor.Data;

namespace MLIDS.Web.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = SettingsItem.Load();

            // Swap out the Class if DAL swap
            var dal = new MongoDAL(settings);

            dal.Initialize();

            services.AddSingleton<BaseDAL>(dal);

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<PacketDataService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}