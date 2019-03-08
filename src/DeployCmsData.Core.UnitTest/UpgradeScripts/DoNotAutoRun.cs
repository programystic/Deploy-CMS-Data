using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    [DoNotAutoRun]
    public class DoNotAutoRun : IUpgradeScript
    {
        public bool RunScript()
        {
            return true;
        }
    }
}
