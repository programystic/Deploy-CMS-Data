using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using Integration.Web.Umb7._13.Constants;

namespace Integration.Web.Umb7._13.UpgradeScripts
{
    [RunScriptEveryTime]
    public class BuildHomepage : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
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

            builder.BuildInFolder("Pages");

            return true;
        }
    }
}
