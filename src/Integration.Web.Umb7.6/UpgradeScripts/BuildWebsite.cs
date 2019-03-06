using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;

namespace Integration.Web.Umb7._6.UpgradeScripts
{
    [RunScriptEveryTime]
    public class BuildWebsite : IUpgradeScript
    {
        public bool RunScript()
        {
            var builder = new DocumentTypeBuilder("websiteRoot");
            builder
                .Name("Website")
                .Icon(Icons.Globe)
                .AddAllowedChildNodeType("homePage")
                .BuildAtRoot();

            return true;
        }
    }
}
