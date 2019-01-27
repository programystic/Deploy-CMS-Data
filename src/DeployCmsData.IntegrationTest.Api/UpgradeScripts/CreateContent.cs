using DeployCmsData.Core.ActionFilters;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    [RunScriptEveryTime]
    public class CreateContent : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var website = GetContentService().CreateContent("My Website", -1, "websiteRoot");
            GetContentService().SaveAndPublishWithStatus(website);

            var homePage = GetContentService().CreateContent("Home", website.Id, "homePage");
            homePage.SetValue("pageTitle", "Hello World");
            GetContentService().SaveAndPublishWithStatus(homePage);

            return true;
        }
    }
}
