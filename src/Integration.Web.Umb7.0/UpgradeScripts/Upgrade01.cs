using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

namespace Integration.Web.Umb7._0.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade01 : IUpgradeScript
    {
        // TODO - Setup constants for all the magic strings below
        public bool RunScript()
        {
            CreatePageMetadata();
            return true;
        }

        public static void CreatePageMetadata()
        {
            var builder = new DocumentTypeBuilder("pageMetaData");
            builder
                .Name("Page Meta Data")
                .Icon(Icons.MindMap)
                .Build();
        }
    }
}