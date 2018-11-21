using System;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Test.UpgradeScripts
{
    public class ReturnsTrue : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
