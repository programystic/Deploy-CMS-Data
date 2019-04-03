using DeployCmsData.UmbracoCms.Models;
using System;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace DeployCmsData.UmbracoCms.Migrations
{
    [Migration("1.0.1", 2, "DeployCmsData")]
    internal class RenameRunTime : MigrationBase
    {
        private readonly UmbracoDatabase _database = ApplicationContext.Current.DatabaseContext.Database;
        private readonly DatabaseSchemaHelper _schemaHelper;

        public RenameRunTime(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
            _schemaHelper = new DatabaseSchemaHelper(_database, logger, sqlSyntax);
        }

        public override void Down()
        {
        }

        public override void Up()
        {
            // var table = _database.Update(new UmbracoUpgradeLog());
        }
    }
}
}
