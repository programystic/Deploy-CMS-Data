using DeployCmsData.Core.Data;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using Umbraco.Core.Composing;
using WebConfigHelper;

namespace DeployCmsData.UmbracoCms.UmbracoComponents
{
    internal class RunOnStartup : IComponent
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

        public void Initialize()
        {
            if (RunAtStartupIsDisabled())
            {
                return;
            }

            RunAllScripts();
        }

        public void Terminate()
        {
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