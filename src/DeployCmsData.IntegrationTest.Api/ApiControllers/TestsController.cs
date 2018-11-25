using System;
using System.Reflection;
using System.Web.Http;
using DeployCmsData.Core.Data;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using Umbraco.Web.WebApi;

namespace DeployCmsData.IntegrationTest.Api.ApiControllers
{
    public class TestsController : UmbracoApiController
    {
        private readonly UpgradeLogRepository _logRepository;
        private readonly UpgradeScriptRepository _scriptRepository;
        private UpgradeScriptManager _upgradeScriptManager;

        public TestsController()
        {
            _logRepository = new UpgradeLogRepository();
            _scriptRepository = new UpgradeScriptRepository();
            _upgradeScriptManager = new UpgradeScriptManager(_logRepository, _scriptRepository);

        }

        [HttpGet]
        public bool RunScript(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var upgradeScript = (IUpgradeScript)assembly.CreateInstance($"DeployCmsData.IntegrationTest.Api.UpgradeScripts.{scriptName}");
            var log = _upgradeScriptManager.RunScript(upgradeScript);
            return log.Success;
        }
    }
}
