using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Marks.Module.Wiki.Migrations.EntityBuilders
{
    public class WikiEntityBuilder : AuditableBaseEntityBuilder<WikiEntityBuilder>
    {
        private const string _entityTableName = "MarksWiki";
        private readonly PrimaryKey<WikiEntityBuilder> _primaryKey = new("PK_MarksWiki", x => x.WikiId);
        private readonly ForeignKey<WikiEntityBuilder> _moduleForeignKey = new("FK_MarksWiki_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public WikiEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override WikiEntityBuilder BuildTable(ColumnsBuilder table)
        {
            WikiId = AddAutoIncrementColumn(table,"WikiId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> WikiId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
