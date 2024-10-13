using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Marks.Module.Wiki.Repository
{
    public class WikiContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Wiki> Wiki { get; set; }

        public WikiContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Wiki>().ToTable(ActiveDatabase.RewriteName("MarksWiki"));
        }
    }
}
