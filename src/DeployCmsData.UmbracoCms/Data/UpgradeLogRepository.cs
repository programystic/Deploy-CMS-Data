using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Persistence;

namespace DeployCmsData.UmbracoCms.Data
{
    public class UpgradeLogRepository : IUpgradeLogRepository
    {
        public IEnumerable<IUpgradeLog> GetLogsByScriptName(string upgradeScriptName)
        {
            using (var scope = Current.ScopeProvider.CreateScope())
            {
                var sql = scope.SqlContext.Sql()
                    .Select<UmbracoUpgradeLog>()
                    .From<UmbracoUpgradeLog>()
                    .Where<UmbracoUpgradeLog>(x => x.UpgradeScriptName == upgradeScriptName);

                return scope.Database.Fetch<UmbracoUpgradeLog>(sql);
            }
        }

        public void SaveLog(IUpgradeLog upgradeLog)
        {
            var umbracoUpgradeLog = new UmbracoUpgradeLog(upgradeLog);

            using (var scope = Current.ScopeProvider.CreateScope())
            {
                scope.Database.Insert(umbracoUpgradeLog);
                scope.Complete();
            }
        }
    }
}