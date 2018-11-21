using DeployCmsData.Core.Models;
using System.Collections.Generic;

namespace DeployCmsData.Core.Interfaces
{
    public interface IUpgradeLogRepository
    {
        IEnumerable<IUpgradeLog> GetLogsByScriptName(string upgradeScriptName);
        void SaveLog(IUpgradeLog upgradeLog);
    }
}