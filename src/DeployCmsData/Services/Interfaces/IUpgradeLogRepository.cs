using DeployCmsData.Models;

namespace DeployCmsData.Services.Interfaces
{
    public interface IUpgradeLogRepository
    {
        UpgradeLog GetLogByScriptName(string upgradeScriptName);
        void SaveLog(UpgradeLog upgradeLog);
    }
}