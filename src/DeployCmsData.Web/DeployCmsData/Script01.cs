using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using Umbraco.Web;

namespace DeployCmsData.Web.DeployCmsData
{
    public class Script01 : IUpgradeScript
    {
        public bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            var builder = new CreateDocumentTypeBuilder(UmbracoContext.Current.Application);
            builder.CreateDocumentTypeAtRoot("HelloWorld", Icons.Ball, "Hello World", "Hello World", true);

            builder.CreateDocumentTypeAtRoot()
                .Alias("HelloWorld")
                .Icon(Icons.Ball)
                .Name("Hello World")
                .AllowedAtRoot()
                .Build();

            return true;
        }
    }
}