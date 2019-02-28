using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._4.ApiController.UpgradeScripts
{
    [RunScriptEveryTime]
    public class BuildWebsite : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new DocumentTypeBuilder("websiteRoot");
            builder
                .Name("Website")
                .Icon(Icons.Globe)
                .AddAllowedChildNodeType("homePage")
                .BuildAtRoot();

            return true;
        }
    }
}
