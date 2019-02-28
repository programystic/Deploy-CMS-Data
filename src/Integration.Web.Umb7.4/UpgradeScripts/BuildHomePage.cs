using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._4.ApiController.UpgradeScripts
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
                .AddComposition("navigationBase")
                .BuildInFolder("Pages");

            return true;
        }
    }
}
