using DeployCmsData.Core.Data;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using System.Reflection;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Integration.Web.Umb7._4.ApiController
{
    // TODO : secure this controller - UmbracoAuthorizedApiController
    public class IntegrationTestController : UmbracoApiController
    {
        private readonly UpgradeLogRepository _logRepository;
        private readonly UpgradeScriptRepository _scriptRepository;
        private UpgradeScriptManager _upgradeScriptManager;

        public IntegrationTestController()
        {
            _logRepository = new UpgradeLogRepository();
            _scriptRepository = new UpgradeScriptRepository();
            _upgradeScriptManager = new UpgradeScriptManager(_logRepository, _scriptRepository);
        }

        [HttpGet]
        public bool RunScript(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var upgradeScript = (IUpgradeScript)assembly.CreateInstance($"Integration.Web.Umb7._4.UpgradeScripts.{scriptName}");
            var log = _upgradeScriptManager.RunScript(upgradeScript);
            return log.Success;
        }
    }
}