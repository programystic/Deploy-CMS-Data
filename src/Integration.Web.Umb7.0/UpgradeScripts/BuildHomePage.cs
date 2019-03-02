using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._0.UpgradeScripts
{
    [RunScriptEveryTime]
    public class BuildHomepage : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeBuilder("homePage");

            builder
                .Name("Home Page")
                .Icon(Icons.Home)
                .AddComposition("pageMetaData")
                .AddComposition("contentBase")
                .AddComposition("navigationBase");

            builder.AddField("mainContent")
                .DataType(DataType.RichTextEditor)
                .Tab("Content");

            builder.BuildInFolder("Pages");

            return true;
        }
    }
}
