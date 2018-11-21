using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.UmbracoCms.UpgradeScripts
{
    [DontAutoRun]
    class ClearTheDecks : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var library = new UmbracoLibrary();
            library.DeleteAllContent();
            library.DeleteAllDocumentTypes();
            library.DeleteAllDocumentTypeFolders();

            return true;
        }
    }
}
