using System.Collections.Generic;
using DeployCmsData.Models;
using DeployCmsData.Services.Interfaces;
using Umbraco.Core;

namespace DeployCmsData.Data
{
    public class UpgradeLogRepository : IUpgradeLogRepository
    {
        public IEnumerable<UpgradeLog> GetLogsByScriptName(string upgradeScriptName)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<UpgradeLog>($"WHERE {nameof(UpgradeLog.UpgradeScriptName)}='{upgradeScriptName}'");
        }

        public void SaveLog(UpgradeLog upgradeLog)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Insert(upgradeLog);
        }
    }
}
