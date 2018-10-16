using DeployCmsData.Models;
using System.Collections.Generic;

namespace DeployCmsData.Interfaces
{
    public interface IUpgradeLogRepository
    {
        IEnumerable<UpgradeLog> GetLogsByScriptName(string upgradeScriptName);
        void SaveLog(UpgradeLog upgradeLog);
    }
}