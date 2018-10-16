namespace DeployCmsData.Interfaces
{
    public interface IUpgradeScript
    {
        bool RunScript(IUpgradeLogRepository upgradeLog);
    }
}