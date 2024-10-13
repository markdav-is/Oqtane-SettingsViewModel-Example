using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace Marks.Module.Wiki.Services
{
    public class WikiService : ServiceBase, IWikiService
    {
        public WikiService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Wiki");

        public async Task<List<Models.Wiki>> GetWikisAsync(int ModuleId)
        {
            List<Models.Wiki> Wikis = await GetJsonAsync<List<Models.Wiki>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Wiki>().ToList());
            return Wikis.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Wiki> GetWikiAsync(int WikiId, int ModuleId)
        {
            return await GetJsonAsync<Models.Wiki>(CreateAuthorizationPolicyUrl($"{Apiurl}/{WikiId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Wiki> AddWikiAsync(Models.Wiki Wiki)
        {
            return await PostJsonAsync<Models.Wiki>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Wiki.ModuleId), Wiki);
        }

        public async Task<Models.Wiki> UpdateWikiAsync(Models.Wiki Wiki)
        {
            return await PutJsonAsync<Models.Wiki>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Wiki.WikiId}", EntityNames.Module, Wiki.ModuleId), Wiki);
        }

        public async Task DeleteWikiAsync(int WikiId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{WikiId}", EntityNames.Module, ModuleId));
        }
    }
}
