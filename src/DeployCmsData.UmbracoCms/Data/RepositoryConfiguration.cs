using DeployCmsData.UmbracoCms.Models;
using System;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace DeployCmsData.UmbracoCms.Data
{
    public sealed class RepositoryConfiguration
    {
        private RepositoryConfiguration()
        {
        }

        public static void SetupDatabase(ApplicationContext applicationContext)
        {
            if (applicationContext == null)
            {
                throw new ArgumentNullException(nameof(applicationContext));
            }

            var context = applicationContext.DatabaseContext;
            var dbHelper = new DatabaseSchemaHelper(context.Database, applicationContext.ProfilingLogger.Logger, context.SqlSyntax);

            if (!dbHelper.TableExist<UmbracoUpgradeLog>())
            {
                dbHelper.CreateTable<UmbracoUpgradeLog>();
            }
        }
    }
}