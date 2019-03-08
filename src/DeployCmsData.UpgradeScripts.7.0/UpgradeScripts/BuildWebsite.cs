using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

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
