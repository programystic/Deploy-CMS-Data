using DeployCmsData.Builders;
using DeployCmsData.Constants;
using DeployCmsData.Services;
using DeployCmsData.Services.Interfaces;

namespace DeployCmsData.Test.UpgradeScripts
{
    public class Upgrade001Test : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            ClearEverythingDown();
            CreateSiteDocumentType();
            CreateFolders();
            CreatePageMetaData();
            return true;
        }

        public void ClearEverythingDown()
        {
            var library = new CmsLibrary();
            library.DeleteAllDocumentTypes();
            library.DeleteAllDocumentTypeFolders();
        }

        public void CreateSiteDocumentType()
        {
            var builder = new DocumentTypeBuilder();
            builder
                .Alias("websiteRoot")
                .Name("Website")
                .Icon(Icons.Globe)
                .BuildAtRoot();
        }

        public void CreateFolders()
        {
            var builder = new DocumentTypeFolderBuilder();

            builder
                .Name("Page Compositions")
                .BuildAtRoot();
        }

        public void CreatePageMetaData()
        {
            var builder = new DocumentTypeBuilder();
            builder
                .Alias("pageMetaData")
                .Name("Page Meta Data")
                .Icon(Icons.MindMap)
                .BuildInFolder("Page Compositions");
        }
    }
}