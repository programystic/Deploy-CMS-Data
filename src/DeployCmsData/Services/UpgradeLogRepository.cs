using System.Data.Entity;
using System.Linq;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;

namespace DeployCmsData.Services
{
    public class UpgradeLogRepository : DbContext, IUpgradeLogRepository
    {
        public UpgradeLogRepository() : base("umbracoDbDSN")
        {
        }

        public DbSet<UpgradeLog> UpgradeLogs { get; set; }

        public UpgradeLog GetLog(string upgradeScriptName)
        {
            return UpgradeLogs.FirstOrDefault(x => x.UpgradeScriptName == upgradeScriptName);
        }

        public void SaveLog(UpgradeLog upgradeLog)
        {
            UpgradeLogs.Add(upgradeLog);
            SaveChanges();
        }
    }
}
