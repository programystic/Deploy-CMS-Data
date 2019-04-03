using DeployCmsData.UmbracoCms.Models;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations.Syntax.Rename;
using Validation;
using System.Linq;

namespace DeployCmsData.UmbracoCms.Data
{
    public sealed class RepositoryConfiguration
    {
        private RepositoryConfiguration()
        {
        }

        public static void SetupDatabase(ApplicationContext applicationContext)
        {
            Requires.NotNull(applicationContext, nameof(applicationContext));

            var context = applicationContext.DatabaseContext;
            var dbHelper = new DatabaseSchemaHelper(context.Database, applicationContext.ProfilingLogger.Logger, context.SqlSyntax);

            if (!dbHelper.TableExist(Constants.Database.LogsTableName))
            {
                dbHelper.CreateTable<UmbracoUpgradeLog>();
            }

            var x = applicationContext.DatabaseContext.
            
        }
    }
}