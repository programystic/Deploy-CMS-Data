using DeployCmsData.Core.Data;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using Umbraco.Core;
using WebConfigHelper;

namespace DeployCmsData.UmbracoCms.Services
{
    internal class RunOnStartup : ApplicationEventHandler
    {
        private WebConfigValues _webConfigValues;

        public RunOnStartup()
        {
            _webConfigValues = new WebConfigValues();
        }

        public RunOnStartup(IWebConfigProvider webConfigProvider)
        {
            _webConfigValues = new WebConfigValues(webConfigProvider);
        }
        
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            MigrationsRunner.Run();

            if (RunAtStartupIsEnabled())
            {
                RunAllScripts();
            }
        }

        private static void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var scriptRepository = new UpgradeScriptRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository, scriptRepository);

            upgradeScriptManager.RunAllScriptsIfNeeded();
        }

        private bool RunAtStartupIsEnabled()
        {
            return !_webConfigValues.GetAppSetting(Constants.AppSettings.DisableRunAtStartup, false);
        }
    }
}