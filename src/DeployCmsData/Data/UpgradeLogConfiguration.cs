using Umbraco.Core;
using Umbraco.Core.Persistence;
using DeployCmsData.Models;

namespace DeployCmsData.Data
{
    public class UpgradeLogConfiguration
    {
        public static void SetupDatabase(ApplicationContext applicationContext)
        {
            var ctx = applicationContext.DatabaseContext;
            var dbHelper = new DatabaseSchemaHelper(ctx.Database, applicationContext.ProfilingLogger.Logger, ctx.SqlSyntax);

            if (!dbHelper.TableExist<UpgradeLog>())
                dbHelper.CreateTable<UpgradeLog>();
        }
    }
}