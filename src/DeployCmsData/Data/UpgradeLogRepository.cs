using DeployCmsData.Models;
using DeployCmsData.Services.Interfaces;
using Umbraco.Core;

namespace DeployCmsData.Data
{
    public class UpgradeLogRepository : IUpgradeLogRepository
    {
        public UpgradeLog GetLogByScriptName(string upgradeScriptName)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;

            var allLogs = db.Fetch<UpgradeLog>($"SELECT * FROM {Constants.Data.LogsTableName}");

            return db.SingleOrDefault<UpgradeLog>($"WHERE {nameof(UpgradeLog.UpgradeScriptName)}='{upgradeScriptName}'");
        }

        public void SaveLog(UpgradeLog upgradeLog)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Insert(upgradeLog);
        }
    }
}
