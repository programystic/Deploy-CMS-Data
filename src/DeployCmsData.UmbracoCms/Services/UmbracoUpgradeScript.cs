using DeployCmsData.Core.Interfaces;

namespace DeployCmsData.UmbracoCms.Services
{
    public class UmbracoUpgradeScript : IUpgradeScript
    {
        public readonly UmbracoLibrary Library;

        public UmbracoUpgradeScript()
        {
            Library = new UmbracoLibrary();
        }

        public virtual bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
