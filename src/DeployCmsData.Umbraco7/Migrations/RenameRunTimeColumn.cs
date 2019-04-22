using DeployCmsData.Umbraco7.Models;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace DeployCmsData.Umbraco7.Migrations
{
    [Migration("1.0.1", 2, "DeployCmsData")]
    public class RenameRunTimeColumn : MigrationBase
    {
        private readonly UmbracoDatabase _database = ApplicationContext.Current.DatabaseContext.Database;
        private readonly DatabaseSchemaHelper _schemaHelper;

        public RenameRunTimeColumn(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
            //_schemaHelper = new DatabaseSchemaHelper(_database, logger, sqlSyntax);
        }

        public override void Up()
        {
            const string oldColumnName = "RuntTimeMilliseconds";

            var columns = SqlSyntax.GetColumnsInSchema(Context.Database).ToArray();

            if (columns.Any(x => x.TableName.InvariantEquals("cmsPropertyData") && x.ColumnName.InvariantEquals("dataDecimal")) == false)
                Create.Column("dataDecimal").OnTable("cmsPropertyData").AsDecimal().Nullable();

            if (!ColumnExistsOnTable(Constants.Database.LogsTableName, nameof(UmbracoUpgradeLog.RunTimeMilliseconds)) &&
                ColumnExistsOnTable(Constants.Database.LogsTableName, oldColumnName))
            {
                Rename
                    .Column(oldColumnName)
                    .OnTable(Constants.Database.LogsTableName)
                    .To(nameof(UmbracoUpgradeLog.RunTimeMilliseconds));
            }
        }

        private bool ColumnExistsOnTable(string tableName, string columnName)
        {            
            var columns = SqlSyntax.GetColumnsInSchema(_database).ToArray();

            return !columns.Any(x => x.TableName.InvariantEquals(tableName)
                && x.ColumnName.InvariantEquals(columnName));
        }

        public override void Down()
        {
        }

    }
}