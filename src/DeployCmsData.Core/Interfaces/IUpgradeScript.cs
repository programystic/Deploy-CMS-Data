namespace DeployCmsData.Core.Interfaces
{
    public interface IUpgradeScript
    {
        bool RunScript(IUpgradeLogRepository upgradeLog);
    }
}