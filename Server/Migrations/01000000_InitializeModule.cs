using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Marks.Module.Wiki.Migrations.EntityBuilders;
using Marks.Module.Wiki.Repository;

namespace Marks.Module.Wiki.Migrations
{
    [DbContext(typeof(WikiContext))]
    [Migration("Marks.Module.Wiki.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new WikiEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new WikiEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}
