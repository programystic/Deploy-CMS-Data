using DeployCmsData.Core.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Services
{
    public class UmbracoUpgradeScript : IUpgradeScript
    {
        private readonly UmbracoLibrary library;
        private readonly IContentService contentService;

        public UmbracoLibrary Library => library;

        public IContentService GetContentService()
        {
            return contentService;
        }

        public UmbracoUpgradeScript()
        {
            library = new UmbracoLibrary();
            contentService = ApplicationContext.Current.Services.ContentService;
        }

        public virtual bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
