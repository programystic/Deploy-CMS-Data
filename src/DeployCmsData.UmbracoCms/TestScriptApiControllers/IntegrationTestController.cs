using DeployCmsData.Core.Data;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using DeployCmsData.UmbracoCms.UpgradeScripts;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace DeployCmsData.UmbracoCms.TestScriptApiControllers
{
    public class IntegrationTestController : UmbracoApiController
    {
        [HttpGet]
        public string ClearTheDecks()
        {
            var logRepository = new UpgradeLogRepository();
            var scriptRepository = new UpgradeScriptRepository();
            var upgradeScriptManager = new UpgradeScriptManager(logRepository, scriptRepository);

            var log = upgradeScriptManager.RunScript(new ClearTheDecks());

            return log.Success ? "True" : log.Exception;
        }
    }
}