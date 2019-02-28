using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._4.ApiController.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade01 : UmbracoUpgradeScript
    {
        // TODO - Setup constants for all the magic strings below
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            CreateFolders();
            CreatePageMetadata();
            return true;
        }

        public static void CreateFolders()
        {
            new DocumentTypeFolderBuilder("Compositions")
                .BuildAtRoot();

            new DocumentTypeFolderBuilder("Pages")
                .BuildAtRoot();
        }

        public static void CreatePageMetadata()
        {
            var builder = new DocumentTypeBuilder("pageMetaData");
            builder
                .Name("Page Meta Data")
                .Icon(Icons.MindMap)
                .BuildInFolder("Compositions");
        }
    }
}