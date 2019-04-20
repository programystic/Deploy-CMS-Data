using DeployCmsData.Umbraco7.Models;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace DeployCmsData.Umbraco7.Migrations
{
    [Migration("1.0.1", 2, "DeployCmsData")]
    public class RenameRunTimeColumn : MigrationBase
    {
        public RenameRunTimeColumn(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Up()
        {
            const string oldColumnName = "RuntTimeMilliseconds";

            var columns = SqlSyntax.GetColumnsInSchema(Context.Database).ToArray();

            if (!ColumnExistsOnTable(Constants.Database.LogsTableName, nameof(UmbracoUpgradeLog.RunTimeMilliseconds)) &&
                ColumnExistsOnTable(Constants.Database.LogsTableName, oldColumnName))
            {
                Rename
                    .Column(oldColumnName)
                    .OnTable(Constants.Database.LogsTableName)
                    .To(nameof(UmbracoUpgradeLog.RunTimeMilliseconds));
            }
        }

        private bool ColumnExistsOnTable(string tableNamne, string columnName)
        {
            var columns = SqlSyntax.GetColumnsInSchema(Context.Database).ToArray();

            return !columns.Any(x => x.TableName.InvariantEquals(tableNamne)
                && x.ColumnName.InvariantEquals(columnName));
        }

        public override void Down()
        {
        }

    }
}