using Umbraco.Core.Migrations;

namespace DeployCmsData.UmbracoCms.UmbracoMigrations
{
    public class UpgradePlan : MigrationPlan
    {
        public UpgradePlan() : base("DeployCmsData")
        {
            From(string.Empty)
            .To<AddLogTable>("AddLogTable");
        }
    }
}