﻿using DeployCmsData.Core.Data;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Services;
using DeployCmsData.UmbracoCms.Data;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace DeployCmsData.UpgradeScripts_8.ApiController
{
    // TODO : secure this controller - UmbracoAuthorizedApiController
    public class IntegrationTestBaseController : UmbracoApiController
    {
        private readonly UpgradeLogRepository _logRepository;
        private readonly UpgradeScriptRepository _scriptRepository;
        private UpgradeScriptManager _upgradeScriptManager;

        public IntegrationTestBaseController()
        {
            _logRepository = new UpgradeLogRepository();
            _scriptRepository = new UpgradeScriptRepository();
            _upgradeScriptManager = new UpgradeScriptManager(_logRepository, _scriptRepository);
        }

        [HttpGet]
        public bool RunScript(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var upgradeScript = (IUpgradeScript)assembly.CreateInstance($"DeployCmsData.UpgradeScripts_8.UpgradeScripts.{scriptName}");
            var log = _upgradeScriptManager.RunScript(upgradeScript);

            Debug.WriteLineIf(!log.Success, log.Exception);

            return log.Success;
        }
    }
}