using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [DoNotAutoRun]
    public class ClearTheDecks : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            Library.DeleteAllContent();
            Library.DeleteAllDocumentTypes();
            Library.DeleteAllDocumentTypeFolders();

            return true;
        }
    }
}
