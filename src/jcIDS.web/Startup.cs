using jcIDS.web.DAL;
using jcIDS.web.Managers;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace jcIDS.web
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
            var configuration = ConfigurationManager.ParseConfiguration(Configuration);

            // TODO Handle Configuration failure with a fall back?

            services.AddSingleton(configuration);

            services.AddMemoryCache();

            services.AddDbContext<IDSContext>(options => options.UseSqlServer(configuration.ObjectValue.DatabaseConnection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#endif
            app.UseStaticFiles();
            
            app.UseMvc();
        }
    }
}