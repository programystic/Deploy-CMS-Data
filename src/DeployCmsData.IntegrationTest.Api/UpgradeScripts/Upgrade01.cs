using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade01 : UmbracoUpgradeScript
    {
        // TODO - Setup constants for all the magic strings below
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            CreateFolders();
            CreatePageMetaData();
            return true;
        }

        public void CreateFolders()
        {
            new DocumentTypeFolderBuilder()
                .Name("Compositions")
                .BuildAtRoot();

            new DocumentTypeFolderBuilder()
                .Name("Pages")
                .BuildAtRoot();

        }

        public void CreatePageMetaData()
        {
            var builder = new DocumentTypeBuilder();
            builder
                .Alias("pageMetaData")
                .Name("Page Meta Data")
                .Icon(Icons.MindMap)
                .BuildInFolder("Compositions");
        }
    }
}