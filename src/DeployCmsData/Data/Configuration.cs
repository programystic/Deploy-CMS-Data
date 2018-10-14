using Umbraco.Core;
using Umbraco.Core.Persistence;
using DeployCmsData.Models;

namespace DeployCmsData.Data
{
    public class Configuration
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


//using DeployCmsData.Data;
//using System.Data.Entity.Migrations;

//namespace DeployCmsData.Data
//{
//    public class Configuration : DbMigrationsConfiguration<UpgradeLogContext>
//    {
//        public Configuration() => AutomaticMigrationsEnabled = true;
//    }
//}
