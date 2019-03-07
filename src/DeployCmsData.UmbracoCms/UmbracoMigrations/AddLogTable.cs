using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Migrations;

namespace DeployCmsData.UmbracoCms.UmbracoMigrations
{
    public class AddLogTable : MigrationBase
    {
        public AddLogTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();

            if (tables.InvariantContains(Constants.Database.LogsTableName))
            {
                return;
            }

            Create.Table<Models.UmbracoUpgradeLog>().Do();
        }
    }
}
