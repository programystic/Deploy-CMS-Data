using DeployCmsData.Core.Data;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using Semver;
using System;
using System.Linq;
using System.Web.Configuration;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Web;

namespace DeployCmsData.UmbracoCms.Services
{
    class RunOnStartup : ApplicationEventHandler
    {
        // TODO - What is the correct event to use that runs when Umbraco is setting up for the first time and fires after the install is fininished>
        // Because this event didn't fire - I think.
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (IsRunAtStartupDisabled()) return;

            RepositoryConfiguration.SetupDatabase(applicationContext);
            RunAllScripts();

            RunMigrations();
        }

        private void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var scriptRepository = new UpgradeScriptRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository, scriptRepository);

            upgradeScriptManager.RunAllScriptsIfNeeded();
        }

        private bool IsRunAtStartupDisabled()
        {
            if (!bool.TryParse(
                WebConfigurationManager.AppSettings[Constants.AppSettings.DisableRunAtStartup],
                out bool disableRunAtStartup))
                disableRunAtStartup = false;

            return disableRunAtStartup;
        }

        private static void RunMigrations()
        {
            const string productName = "DeployCmsData";
            var currentVersion = new SemVersion(0, 0, 0);

            // get all migrations for "Statistics" already executed
            var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(productName);

            // get the latest migration for "Statistics" executed
            var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

            if (latestMigration != null)
                currentVersion = latestMigration.Version;

            var targetVersion = new SemVersion(1, 0, 0);
            if (targetVersion == currentVersion)
                return;

            var migrationsRunner = new MigrationRunner(
              ApplicationContext.Current.Services.MigrationEntryService,
              ApplicationContext.Current.ProfilingLogger.Logger,
              currentVersion,
              targetVersion,
              productName);

            try
            {
                migrationsRunner.Execute(UmbracoContext.Current.Application.DatabaseContext.Database);
            }
            catch (Exception e)
            {
                LogHelper.Error<RunOnStartup>("Error running Statistics migration", e);
            }
        }
    }
}