using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UnitTest.UpgradeScripts
{
    public class ReturnsTrue : IUpgradeScript
    {
        public bool RunScript()
        {
            return true;
        }
    }
}
