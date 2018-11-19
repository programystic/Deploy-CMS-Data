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
            var upgradeScriptManager = new UpgradeScriptManager(logRepository);

            var log = upgradeScriptManager.RunScript(new Upgrade01());

            return log.Success ? "True" : log.Exception;
        }
    }
}
