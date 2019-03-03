using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._13.UpgradeScripts
{
    [DoNotAutoRun]
    public class ClearTheDecks : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            Library.DeleteAllContent();
            Library.DeleteAllDocumentTypes();
            Library.DeleteAllDocumentTypeFolders();
            //Library.DeleteAllTemplates();

            return true;
        }
    }
}
