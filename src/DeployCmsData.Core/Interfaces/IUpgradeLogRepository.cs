using DeployCmsData.Core.Models;
using System.Collections.Generic;

namespace DeployCmsData.Core.Interfaces
{
    public interface IUpgradeLogRepository
    {
        IEnumerable<UpgradeLog> GetLogsByScriptName(string upgradeScriptName);
        void SaveLog(UpgradeLog upgradeLog);
    }
}