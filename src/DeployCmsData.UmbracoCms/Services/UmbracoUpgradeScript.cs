using DeployCmsData.Core.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Services
{
    public class UmbracoUpgradeScript : IUpgradeScript
    {
        public readonly UmbracoLibrary Library;
        public readonly IContentService ContentService;

        public UmbracoUpgradeScript()
        {
            Library = new UmbracoLibrary();
            ContentService = ApplicationContext.Current.Services.ContentService;
        }

        
        public virtual bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
