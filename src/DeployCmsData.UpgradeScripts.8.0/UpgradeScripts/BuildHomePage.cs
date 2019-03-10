using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UpgradeScripts_8.Constants;

namespace DeployCmsData.UpgradeScripts_8.UpgradeScripts
{
    [DoNotAutoRun]
    public class BuildHomepage : IUpgradeScript
    {
        public bool RunScript()
        {
            var builder = new DocumentTypeBuilder("homePage");

            builder
                .Name("Home Page")
                .Icon(Icons.Home)
                .AddComposition("pageMetaData")
                .AddComposition("contentBase")
                .AddComposition("navigationBase");

            builder.AddField("mainContent")
                .DataType(LocalDataTypes.Grid)
                .Tab("Content");

            builder.AddField("additionalContent")
                .DataType(LocalDataTypes.Grid)
                .Tab("Content 2");

            builder.BuildInFolder("Pages");
            return true;
        }
    }
}