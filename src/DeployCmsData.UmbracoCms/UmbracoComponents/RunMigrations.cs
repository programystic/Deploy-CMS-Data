using DeployCmsData.UmbracoCms.UmbracoMigrations;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UmbracoComponents
{
    public class RunMigrations : IComponent
    {
        public void Initialize()
        {
            var migrationBuilder = Current.Factory.GetInstance<IMigrationBuilder>();
            var keyValueService = Current.Factory.GetInstance<IKeyValueService>();
            var upgrader = new Upgrader(new UpgradePlan());

            upgrader.Execute(Current.ScopeProvider, migrationBuilder, keyValueService, Current.Logger);
        }

        public void Terminate()
        {
        }
    }
}