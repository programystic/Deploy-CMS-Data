using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [Migration("1.0.0", 1, "DeployCmsData")]
    public  class Migration01 : MigrationBase
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

    [Migration("1.3.0", 2, "DeployCmsData")]
    public class Migration03 : MigrationBase
    {
        public Migration03(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
        }


        public override void Up()
        {
        }
    }

}
