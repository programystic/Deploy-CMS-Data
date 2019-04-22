using DeployCmsData.Umbraco7.Models;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace DeployCmsData.Umbraco7.Migrations
{
    [Migration("1.0.0", 1, "DeployCmsData")]
    public class CreateLogTable : MigrationBase
    {
        private readonly DatabaseSchemaHelper _schemaHelper;

        public CreateLogTable(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
            _schemaHelper = new DatabaseSchemaHelper(ApplicationContext.Current.DatabaseContext.Database, logger, sqlSyntax);
        }

        public override void Up()
        {
            if (!_schemaHelper.TableExist(Constants.Database.LogsTableName))
            {
                _schemaHelper.CreateTable<UmbracoUpgradeLog>();
            }
        }

        public override void Down()
        {
        }
    }
}