using DeployCmsData.UmbracoCms.Models;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web.Composing;
using Validation;

namespace DeployCmsData.UmbracoCms.Data
{
    public sealed class RepositoryConfiguration
    {
        private RepositoryConfiguration()
        {
        }

        public static void SetupDatabase(Umbraco.Web.UmbracoApplication applicationContext)
        {
            Requires.NotNull(applicationContext, nameof(applicationContext));

            var context = applicationContext.DatabaseContext;
            var dbHelper = new DatabaseSchemaHelper(context.Database, applicationContext.ProfilingLogger.Logger, context.SqlSyntax);

            if (!dbHelper.TableExist(Constants.Database.LogsTableName))
            {
                dbHelper.CreateTable<UmbracoUpgradeLog>();
            }
        }
    }
}