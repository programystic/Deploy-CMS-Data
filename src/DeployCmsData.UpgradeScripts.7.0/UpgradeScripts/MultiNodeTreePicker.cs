using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Services;
using System;
using Umbraco.Core;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class MultiNodeTreePicker : IUpgradeScript
    {
        public bool RunScript()
        {
            var id = Guid.Parse("{60FFCA99-3B98-49EA-9F64-E4C69BB00285}");
            var library = new UmbracoLibrary();

            var contentService = ApplicationContext.Current.Services.ContentService;
            var website = contentService.CreateContent("My Website 2", -1, "websiteRoot");
            contentService.SaveAndPublishWithStatus(website);

            var homePage = contentService.CreateContent("Home", website.Id, "homePage");
            homePage.SetValue("pageTitle", "Hello World");
            contentService.SaveAndPublishWithStatus(homePage);

            library.DeleteDataTypeById(id);
            var builder = new MultiNodeTreePickerBuilder(id);

            builder
                .Name("Another Multi Node Tree Picker")
                .AllowItemsOfType("websiteRoot", "homePage")
                .MinimumNumberOfItems(1)
                .MaximumNumberOfItems(5)
                .ShowOpenButton()
                .StartNodeContent(homePage.Id)
                .Build();

            return true;
        }
    }
}
