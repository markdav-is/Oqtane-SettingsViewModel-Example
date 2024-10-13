using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marks.Module.Wiki.Services
{
    public interface IWikiService 
    {
        Task<List<Models.Wiki>> GetWikisAsync(int ModuleId);

        Task<Models.Wiki> GetWikiAsync(int WikiId, int ModuleId);

        Task<Models.Wiki> AddWikiAsync(Models.Wiki Wiki);

        Task<Models.Wiki> UpdateWikiAsync(Models.Wiki Wiki);

        Task DeleteWikiAsync(int WikiId, int ModuleId);
    }
}
