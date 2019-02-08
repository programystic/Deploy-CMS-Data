using DeployCmsData.Core.Data;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using Semver;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Web;
using WebConfigHelper;

namespace DeployCmsData.UmbracoCms.Services
{
    internal class RunOnStartup : ApplicationEventHandler
    {
        private WebConfigValues _webConfigValues;

        public RunOnStartup()
        {
            _webConfigValues = new WebConfigValues();
        }

        // TODO - What is the correct event to use that runs when Umbraco is setting up for the first time and fires after the install is fininished>
        // Because this event didn't fire - I think.
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (RunAtStartupIsDisabled())
            {
                return;
            }

            RepositoryConfiguration.SetupDatabase(applicationContext);
            RunAllScripts();

            RunMigrations();
        }

        private static void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var scriptRepository = new UpgradeScriptRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository, scriptRepository);

            upgradeScriptManager.RunAllScriptsIfNeeded();
        }

        private bool RunAtStartupIsDisabled()
        {
            return _webConfigValues.GetAppSetting(Constants.AppSettings.DisableRunAtStartup, false);
        }

        // Take a look at Umbraco.Core.Persistence.Migrations source code
        // See what it can do

        private static void RunMigrations()
        {
            const string productName = "DeployCmsData";
            var currentVersion = new SemVersion(0, 0, 0);

            // get all migrations for "Statistics" already executed
            var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(productName);

            // get the latest migration for "Statistics" executed
            var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

            if (latestMigration != null)
            {
                currentVersion = latestMigration.Version;
            }

            var targetVersion = new SemVersion(2, 0, 0);
            if (targetVersion == currentVersion)
            {
                return;
            }

            var migrationsRunner = new MigrationRunner(
              ApplicationContext.Current.Services.MigrationEntryService,
              ApplicationContext.Current.ProfilingLogger.Logger,
              currentVersion,
              targetVersion,
              productName);

            migrationsRunner.Execute(UmbracoContext.Current.Application.DatabaseContext.Database);
        }

        [Migration("1.0.0", 1, "DeployCmsData")]
        public class Migration01 : MigrationBase
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
}