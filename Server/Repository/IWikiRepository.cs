using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marks.Module.Wiki.Repository
{
    public interface IWikiRepository
    {
        IEnumerable<Models.Wiki> GetWikis(int ModuleId);
        Models.Wiki GetWiki(int WikiId);
        Models.Wiki GetWiki(int WikiId, bool tracking);
        Models.Wiki AddWiki(Models.Wiki Wiki);
        Models.Wiki UpdateWiki(Models.Wiki Wiki);
        void DeleteWiki(int WikiId);
    }
}
