using DeployCmsData.UmbracoCms.Models;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace DeployCmsData.UmbracoCms.Data
{
    public class RepositoryConfiguration
    {
        public static void SetupDatabase(ApplicationContext applicationContext)
        {
            var context = applicationContext.DatabaseContext;
            var dbHelper = new DatabaseSchemaHelper(context.Database, applicationContext.ProfilingLogger.Logger, context.SqlSyntax);

            if (!dbHelper.TableExist<UmbracoUpgradeLog>())
                dbHelper.CreateTable<UmbracoUpgradeLog>();
        }
    }
}