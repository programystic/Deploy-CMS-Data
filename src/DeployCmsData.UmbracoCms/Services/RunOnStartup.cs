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

        // TODO - What is the correct event to use that runs when Umbraco is setting up for the first time and fires after the install is fininished>
        // Because this event didn't fire - I think.
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (RunAtStartupIsDisabled())
            {
                return;
            }

            RepositoryConfiguration.SetupDatabase(applicationContext);
            RunAllScripts();
        }

        private static void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var scriptRepository = new UpgradeScriptRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository, scriptRepository);

            upgradeScriptManager.RunAllScriptsIfNeeded();
        }

        private bool RunAtStartupIsDisabled()
        {
            return _webConfigValues.GetAppSetting(Constants.AppSettings.DisableRunAtStartup, false);
        }
    }
}