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
            const string productName = "ProductName";
            var currentVersion = new SemVersion(0, 0, 0);

            // get all migrations already executed
            var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(productName);

            // get the latest migration executed
            var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

            if (latestMigration != null)
            {
                currentVersion = latestMigration.Version;
            }

            var targetVersion = new SemVersion(1, 0, 0);
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

            try
            {
                migrationsRunner.Execute(UmbracoContext.Current.Application.DatabaseContext.Database);
            }
            catch (Exception e)
            {
                LogHelper.Error<MigrationsRunner>("Error running Statistics migration", e);
            }
        }
    }
}
