using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using Umbraco.Core.Composing;

namespace DeployCmsData.UpgradeScripts_8.UpgradeScripts
{
    [DoNotAutoRun]
    public class CreateContent : IUpgradeScript
    {
        public bool RunScript()
        {
            var contentService = Current.Services.ContentService;

            var website = contentService.CreateAndSave("My Website", -1, "websiteRoot");

            var homePage = contentService.Create("Home", website.Id, "homePage");
            homePage.SetValue("pageTitle", "Hello World");
            contentService.SaveAndPublish(homePage);

            return true;
        }
    }
}
