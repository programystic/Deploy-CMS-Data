using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._6.UpgradeScripts
{
    [DoNotAutoRun]
    public class ClearTheDecks : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var library = new UmbracoLibrary();

            library.DeleteAllContent();
            library.DeleteAllDocumentTypes();
            library.DeleteAllDocumentTypeFolders();
            //Library.DeleteAllTemplates();

            return true;
        }
    }
}
