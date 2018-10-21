using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using System.Web.Configuration;
using Umbraco.Core;

namespace DeployCmsData.UmbracoCms.Services
{
    class RunOnStartup : ApplicationEventHandler
    {
        // TODO - What is the correct event to use that runs when Umbraco is setting up for the first time and fires after the install is fininished>
        // Because this event didn't fire - I think.
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (IsRunAtStartupDisabled()) return;

            RepositoryConfiguration.SetupDatabase(applicationContext);
            RunAllScripts();
        }

        private void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository);

            upgradeScriptManager.RunAllScriptsIfNeeded();
        }

        private bool IsRunAtStartupDisabled()
        {
            if (!bool.TryParse(
                WebConfigurationManager.AppSettings[Constants.AppSettings.DisableRunAtStartup],
                out bool disableRunAtStartup))
                disableRunAtStartup = false;

            return disableRunAtStartup;
        }
    }
}