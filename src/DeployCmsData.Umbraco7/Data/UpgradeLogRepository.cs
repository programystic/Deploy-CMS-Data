using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using DeployCmsData.Umbraco7.Models;
using System.Collections.Generic;
using Umbraco.Core;

namespace DeployCmsData.Umbraco7.Data
{
    public class UpgradeLogRepository : IUpgradeLogRepository
    {
        public IEnumerable<IUpgradeLog> GetLogsByScriptName(string upgradeScriptName)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<UmbracoUpgradeLog>($"WHERE {nameof(UpgradeLog.UpgradeScriptName)}='{upgradeScriptName}'");
        }

        public void SaveLog(IUpgradeLog upgradeLog)
        {
            var newLog = new UmbracoUpgradeLog(upgradeLog);

            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Insert(newLog);
        }
    }
}