//using System.Data.Entity;
//using System.Linq;
//using DeployCmsData.Models;
//using DeployCmsData.Services.Interfaces;

//namespace DeployCmsData.Data
//{
//    public class UpgradeLogContext : DbContext, IUpgradeLogRepository
//    {
//        public UpgradeLogContext() 
//            : base(Umbraco.Core.Constants.System.UmbracoConnectionName)
//        {
//            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UpgradeLogContext, Configuration>());
//        }

//        public DbSet<UpgradeLog> UpgradeLogs { get; set; }

//        public UpgradeLog GetLogByScriptName(string upgradeScriptName) 
//            => UpgradeLogs
//            .FirstOrDefault(x => x.UpgradeScriptName == upgradeScriptName);
        
//        public void SaveLog(UpgradeLog upgradeLog)
//        {
//            UpgradeLogs.Add(upgradeLog);
//            SaveChanges();
//        }
//    }
//}