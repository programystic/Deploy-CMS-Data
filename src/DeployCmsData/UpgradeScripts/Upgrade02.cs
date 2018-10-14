using DeployCmsData.ActionFilters;
using DeployCmsData.Builders;
using DeployCmsData.Constants;
using DeployCmsData.Services.Interfaces;

namespace DeployCmsData.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade02 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            BuildContentBase();
            BuildNavigationBase();

            return true;
        }

        public void BuildContentBase()
        {
            var builder = new DocumentTypeBuilder();

            builder
                .Alias("contentBase")
                .Name("Content Base")
                .Icon(Constants.Icons.Document);

            builder.AddField()
                .Alias("pageTitle")
                .Name("Page Title")
                .Description("The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters")
                .DataType(CmsDataType.TextString)
                .IsMandatory()
                .Tab("Content");

            builder.AddField()
                .Alias("bodyText")
                .Name("Content")
                .DataType(CmsDataType.Textarea)
                .Tab("Content");

            builder.BuildInFolder("Compositions");
        }

        public void BuildNavigationBase()
        {
            var builder = new DocumentTypeBuilder();

            builder
                .Alias("navigationBase")
                .Name("Navigation Base")
                .Icon(Constants.Icons.Nodes);

            builder.AddField()
                .Alias("seoMetaDescription")
                .Name("Description")
                .Description("A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130")
                .DataType(CmsDataType.Textarea)
                .Tab("Navigation & SEO");

            builder.AddField()
                .Alias("keywords")
                .Name("Keywords")
                .Description("Keywords that describe the content of the page. This is consired optional since most modern search engines don't use this anymore")
                .DataType(CmsDataType.Tags)
                .Tab("Navigation & SEO");

            builder.AddField()
                .Alias("umbracoNavihide")
                .Name("Hide in Navigation")
                .Description("If you don't want this page to appear in the navigation, check this box")
                .DataType(CmsDataType.Checkbox)
                .Tab("Navigation & SEO");

            builder.BuildInFolder("Compositions");
        }
    }
}
