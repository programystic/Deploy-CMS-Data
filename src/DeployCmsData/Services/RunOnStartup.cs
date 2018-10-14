using DeployCmsData.Data;
using System.Web.Configuration;
using Umbraco.Core;

namespace DeployCmsData.Services
{
    class RunOnStartup : ApplicationEventHandler
    {
        // TODO - What is the correct event to use that runs when Umbraco is setting up for the first time and fires after the install is fininished>
        // Because this event didn't fire - I think.
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (IsRunAtStartupDisabled()) return;

            UpgradeLogConfiguration.SetupDatabase(applicationContext);
            RunAllScripts();
        }

        private void RunAllScripts()
        {
            var logRepository = new UpgradeLogRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository);

            upgradeScriptManager.RunScripts();
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