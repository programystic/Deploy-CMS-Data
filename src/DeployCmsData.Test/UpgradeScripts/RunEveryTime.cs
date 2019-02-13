using System;
using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    [RunScriptEveryTime]
    public class RunEveryTime : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
