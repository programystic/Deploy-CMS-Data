using DeployCmsData.Models;

namespace DeployCmsData.Interfaces
{
    public interface IUpgradeLogRepository
    {
        UpgradeLog GetLog(string upgradeScriptName);
        void SaveLog(UpgradeLog upgradeLog);
    }
}