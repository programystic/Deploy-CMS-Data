using Semver;
using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Web;

namespace DeployCmsData.Umbraco7.Services
{
    internal class MigrationsRunner
    {
        public const string ProductName = "DeployCmsData";

        public static void Run()
        {
            var currentVersion = new SemVersion(0, 0, 0);

            // get all migrations already executed
            var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(ProductName);

            // get the latest migration executed
            var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

            if (latestMigration != null)
            {
                currentVersion = latestMigration.Version;
            }

            var targetVersion = new SemVersion(1, 0, 1);
            if (targetVersion == currentVersion)
            {
                return;
            }

            var migrationsRunner = new MigrationRunner(
               ApplicationContext.Current.Services.MigrationEntryService,
               ApplicationContext.Current.ProfilingLogger.Logger,
               currentVersion,
               targetVersion,
               ProductName);

            try
            {
                migrationsRunner.Execute(UmbracoContext.Current.Application.DatabaseContext.Database);
            }
            catch (System.Web.HttpException e)
            {
                // because umbraco runs some other migrations after the migration runner 
                // is executed we get httpexception
                // catch this error, but don't do anything
                // fixed in 7.4.2+ see : http://issues.umbraco.org/issue/U4-8077
            }
            catch (Exception e)
            {
                LogHelper.Error<MigrationsRunner>($"Error running migrations: {e.Message}", e);
            }
        }
    }
}
