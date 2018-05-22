using DeployCmsData.Services;
using Umbraco.Core;

namespace DeployCmsData.Web.DeployCmsData
{
    public class Startup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var manager = new UpgradeScriptManager();
            manager.RunScript(new Script01());
            manager.RunScript(new Script02());
            manager.RunScript(new Script03());
        }
    }
}