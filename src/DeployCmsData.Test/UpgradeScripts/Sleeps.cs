using System.Threading;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    public class Sleeps : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            Thread.Sleep(1);

            return true;
        }
    }
}
