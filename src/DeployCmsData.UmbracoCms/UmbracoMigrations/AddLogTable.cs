using DeployCmsData.UmbracoCms.Models;
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
            if (!TableExists(Constants.Database.LogsTableName))
            {
                Create.Table<UmbracoUpgradeLog>();
            }
        }
    }
}
