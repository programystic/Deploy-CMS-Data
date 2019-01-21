using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [Migration("1.0.0", 1, "DeployCmsData")]
    internal class Migration01 : MigrationBase
    {
        public Migration01(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
        }

        public override void Up()
        {
        }
    }

    [Migration("1.1.0", 2, "DeployCmsData")]
    internal class Migration02 : MigrationBase
    {
        public Migration02(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }


        public override void Up()
        {
            throw new System.NotImplementedException();
        }
    }

}
