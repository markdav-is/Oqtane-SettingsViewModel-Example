using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace Marks.Module.Wiki.Repository
{
    public class WikiRepository : IWikiRepository, ITransientService
    {
        private readonly IDbContextFactory<WikiContext> _factory;

        public WikiRepository(IDbContextFactory<WikiContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Wiki> GetWikis(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Wiki.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Wiki GetWiki(int WikiId)
        {
            return GetWiki(WikiId, true);
        }

        public Models.Wiki GetWiki(int WikiId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Wiki.Find(WikiId);
            }
            else
            {
                return db.Wiki.AsNoTracking().FirstOrDefault(item => item.WikiId == WikiId);
            }
        }

        public Models.Wiki AddWiki(Models.Wiki Wiki)
        {
            using var db = _factory.CreateDbContext();
            db.Wiki.Add(Wiki);
            db.SaveChanges();
            return Wiki;
        }

        public Models.Wiki UpdateWiki(Models.Wiki Wiki)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Wiki).State = EntityState.Modified;
            db.SaveChanges();
            return Wiki;
        }

        public void DeleteWiki(int WikiId)
        {
            using var db = _factory.CreateDbContext();
            Models.Wiki Wiki = db.Wiki.Find(WikiId);
            db.Wiki.Remove(Wiki);
            db.SaveChanges();
        }
    }
}
