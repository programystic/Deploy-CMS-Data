using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    [DoNotAutoRun]
    public class DoNotAutoRun : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
