using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Test.UpgradeScripts
{
    public class ReturnsFalse : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return false;
        }
    }
}
