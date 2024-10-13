using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Marks.Module.Wiki.Repository;
using Marks.Module.Wiki.Services;

namespace Marks.Module.Wiki.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IWikiService, ServerWikiService>();
            services.AddDbContextFactory<WikiContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
