using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using DeployCmsData.UmbracoCms.Models;
using System.Collections.Generic;
using Umbraco.Core;

namespace DeployCmsData.UmbracoCms.Data
{
    public class UpgradeLogRepository : IUpgradeLogRepository
    {
        public IEnumerable<UpgradeLog> GetLogsByScriptName(string upgradeScriptName)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            return db.Fetch<UmbracoUpgradeLog>($"WHERE {nameof(UpgradeLog.UpgradeScriptName)}='{upgradeScriptName}'");
        }

        public void SaveLog(UpgradeLog upgradeLog)
        {
            var db = ApplicationContext.Current.DatabaseContext.Database;
            db.Insert((UmbracoUpgradeLog)upgradeLog);
        }
    }
}