using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace Integration.Web.Umb7._4.ApiController.UpgradeScripts
{
    [RunScriptEveryTime]
    public class CreateContent : UmbracoUpgradeScript
    {
        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {            
            var website = ContentService.CreateContent("My Website", -1, "websiteRoot");
            ContentService.SaveAndPublishWithStatus(website);

            var homePage = ContentService.CreateContent("Home", website.Id, "homePage");
            homePage.SetValue("pageTitle", "Hello World");
            ContentService.SaveAndPublishWithStatus(homePage);

            return true;
        }
    }
}
