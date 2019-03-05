using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

namespace Integration.Web.Umb7._0.UpgradeScripts
{
    [RunScriptEveryTime]
    public class BuildHomepage : IUpgradeScript
    {
        public bool RunScript()
        {

            new DocumentTypeBuilder("bleugh").Build();

            var builder = new DocumentTypeBuilder("homePage");

            builder
                .Name("Home Page")
                .Icon(Icons.Home);

            builder.AddField("mainContent")
                .DataType(DataType.RichTextEditor)
                .Tab("Content");

            builder.Build();

            return true;
        }
    }
}
