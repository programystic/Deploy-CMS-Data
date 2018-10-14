namespace DeployCmsData.Services.Interfaces
{
    public interface IUpgradeScript
    {
        bool RunScript(IUpgradeLogRepository upgradeLog);
    }
}