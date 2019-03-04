using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    public class ReturnsFalse : IUpgradeScript
    {
        public bool RunScript()
        {
            return false;
        }
    }
}
