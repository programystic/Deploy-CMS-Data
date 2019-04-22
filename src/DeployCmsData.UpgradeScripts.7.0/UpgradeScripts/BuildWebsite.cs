using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class BuildWebsite : IUpgradeScript
    {
        public bool RunScript()
        {
            var builder = new DocumentTypeBuilder("websiteRoot");
            builder
                .Name("Website")
                .Icon(Icons.Globe)
                .AddAllowedChildNodeType("homePage")
                .AllowedAsRoot()
                .BuildAtRoot();

            return true;
        }
    }
}
