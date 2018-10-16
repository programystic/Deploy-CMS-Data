using System.Collections.Generic;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using DeployCmsData.UmbracoCms.Models;
using Umbraco.Core;

namespace DeployCmsData.UmbracoCms.Data
{
    public class UpgradeLogRepository : IUpgradeLogRepository
    {
        public IEnumerable<UpgradeLog> GetLogsByScriptName(string upgradeScriptName)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<UmbracoUpgradeLog>($"WHERE {nameof(UmbracoUpgradeLog.UpgradeScriptName)}='{upgradeScriptName}'");
        }

        public void SaveLog(UpgradeLog upgradeLog)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Insert(upgradeLog);
        }
    }
}
