using Umbraco.Core;

namespace DeployCmsData.Services
{
    class RunOnStartup : ApplicationEventHandler
    {
        protected override void ApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationInitialized(umbracoApplication, applicationContext);
        }

        private void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository);
            upgradeScriptManager.RunAllScripts();
        }
    }
}
