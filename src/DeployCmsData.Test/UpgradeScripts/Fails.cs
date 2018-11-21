using System;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.Test.UpgradeScripts
{
    public class Fails : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            throw new InvalidProgramException();
        }
    }
}