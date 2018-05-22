using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Umbraco.Web;

namespace DeployCmsData.Web.DeployCmsData
{
    public class Script02 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new CreateDocumentTypeBuilder(UmbracoContext.Current.Application);
            builder.CreateDocumentType("HelloWorld", "HelloWorld2", Icons.Ball, "Hello World 2", "Hello World 2", true);

            return true;
        }
    }
}