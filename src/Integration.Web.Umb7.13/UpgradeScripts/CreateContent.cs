using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;
using Umbraco.Core;

namespace Integration.Web.Umb7._13.UpgradeScripts
{
    [RunScriptEveryTime]
    public class CreateContent : IUpgradeScript
    {
        public bool RunScript()
        {
            var contentService = ApplicationContext.Current.Services.ContentService;

            var website = contentService.CreateContent("My Website", -1, "websiteRoot");
            contentService.SaveAndPublishWithStatus(website);

            var homePage = contentService.CreateContent("Home", website.Id, "homePage");
            homePage.SetValue("pageTitle", "Hello World");
            contentService.SaveAndPublishWithStatus(homePage);

            return true;
        }
    }
}
