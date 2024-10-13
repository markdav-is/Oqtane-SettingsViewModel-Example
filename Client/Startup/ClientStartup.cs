using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using Marks.Module.Wiki.Services;

namespace Marks.Module.Wiki.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWikiService, WikiService>();
        }
    }
}
