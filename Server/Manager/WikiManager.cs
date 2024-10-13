using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using Marks.Module.Wiki.Repository;
using System.Threading.Tasks;

namespace Marks.Module.Wiki.Manager
{
    public class WikiManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IWikiRepository _WikiRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public WikiManager(IWikiRepository WikiRepository, IDBContextDependencies DBContextDependencies)
        {
            _WikiRepository = WikiRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new WikiContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new WikiContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Wiki> Wikis = _WikiRepository.GetWikis(module.ModuleId).ToList();
            if (Wikis != null)
            {
                content = JsonSerializer.Serialize(Wikis);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Wiki> Wikis = null;
            if (!string.IsNullOrEmpty(content))
            {
                Wikis = JsonSerializer.Deserialize<List<Models.Wiki>>(content);
            }
            if (Wikis != null)
            {
                foreach(var Wiki in Wikis)
                {
                    _WikiRepository.AddWiki(new Models.Wiki { ModuleId = module.ModuleId, Name = Wiki.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Wiki in _WikiRepository.GetWikis(pageModule.ModuleId))
           {
               if (Wiki.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "MarksWiki",
                       EntityId = Wiki.WikiId.ToString(),
                       Title = Wiki.Name,
                       Body = Wiki.Name,
                       ContentModifiedBy = Wiki.ModifiedBy,
                       ContentModifiedOn = Wiki.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
