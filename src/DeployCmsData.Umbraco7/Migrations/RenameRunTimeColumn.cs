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
        const string OldColumnName = "RuntTimeMilliseconds";

        private readonly UmbracoDatabase _database = ApplicationContext.Current.DatabaseContext.Database;

        public RenameRunTimeColumn(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Up()
        {

            var columns = SqlSyntax.GetColumnsInSchema(_database).ToArray();

            var columnExists = columns.Any(x => x.TableName.InvariantEquals(Constants.Database.LogsTableName)
                   && x.ColumnName.InvariantEquals(OldColumnName));

            if (columnExists)
            {
                var provider = _database.GetDatabaseProvider();

                if (provider == DatabaseProviders.SqlServerCE)
                {
                    RenameSqlServerCeColumn();
                }
                else
                {
                    RenameColumn();
                }
            }
        }

        private void RenameColumn()
        {
            Rename
                .Column(OldColumnName)
                .OnTable(Constants.Database.LogsTableName)
                .To(nameof(UmbracoUpgradeLog.RunTimeMilliseconds));
        }

        private void RenameSqlServerCeColumn()
        {
            Create
                .Column(nameof(UmbracoUpgradeLog.RunTimeMilliseconds))
                .OnTable(Constants.Database.LogsTableName)
                .AsInt32()
                .Nullable();

            RenameColumn();

            Delete
                .Column(OldColumnName)
                .FromTable(Constants.Database.LogsTableName);
        }

        public override void Down()
        {
        }

    }
}