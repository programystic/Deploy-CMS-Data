using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Services
{
    [DoNotAutoRun]
    public class UmbracoUpgradeScript : IUpgradeScript
    {
        public UmbracoLibrary Library { get; }

#pragma warning disable CS3003 // Type is not CLS-compliant
        public IContentService ContentService { get; }
#pragma warning restore CS3003 // Type is not CLS-compliant

        public UmbracoUpgradeScript()
        {
            Library = new UmbracoLibrary();
            ContentService = Current.Services.ContentService;
        }

        public virtual bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            return true;
        }
    }
}
