using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;
using DeployCmsData.UpgradeScripts_7.Constants;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
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
                .DataTypeAlias(LocalDataTypes.Grid)
                .Tab("Content");

            builder.AddField("additionalContent")
                .DataTypeAlias(LocalDataTypes.Grid)
                .Tab("Content 2");

            builder.BuildInFolder("Pages");
            return true;
        }
    }
}