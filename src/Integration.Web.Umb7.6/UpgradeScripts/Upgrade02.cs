using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

namespace Integration.Web.Umb7._6.UpgradeScripts
{
    [RunScriptEveryTime]
    public class Upgrade02 : IUpgradeScript
    {
        public bool RunScript()
        {
            BuildContentBase();
            BuildNavigationBase();

            return true;
        }

        public static void BuildContentBase()
        {
            var builder = new DocumentTypeBuilder("contentBase");

            builder
                .Name("Content Base")
                .Icon(Icons.Document);

            builder.AddField("pageTitle")
                .Name("Page Title")
                .Description("The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters")
                .IsMandatory()
                .Tab("Content");

            builder.AddField("bodyText")
                .Name("Content")
                .DataType(DataType.RichTextEditor)
                .Tab("Content");

            builder.BuildInFolder("Compositions");
        }

        public static void BuildNavigationBase()
        {
            var builder = new DocumentTypeBuilder("navigationBase");

            builder
                .Name("Navigation Base")
                .Icon(Icons.Nodes);

            builder.AddField("seoMetaDescription")
                .Name("Description")
                .Description("A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130")
                .DataType(DataType.TextArea)
                .Tab("Navigation & SEO");

            builder.AddField("keywords")
                .Name("Keywords")
                .Description("Keywords that describe the content of the page. This is consired optional since most modern search engines don't use this anymore")
                .DataType(DataType.Tags)
                .Tab("Navigation & SEO");

            builder.AddField("umbracoNavihide")
                .Name("Hide in Navigation")
                .Description("If you don't want this page to appear in the navigation, check this box")
                .DataType(DataType.CheckBox)
                .Tab("Navigation & SEO");

            builder.BuildInFolder("Compositions");
        }

    }
}
