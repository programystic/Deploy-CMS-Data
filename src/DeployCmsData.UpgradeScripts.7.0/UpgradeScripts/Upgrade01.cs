using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

namespace Integration.Web.Umb7._4.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade01 : IUpgradeScript
    {
        // TODO - Setup constants for all the magic strings below
        public bool RunScript()
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