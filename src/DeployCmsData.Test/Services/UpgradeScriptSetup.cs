using System;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using DeployCmsData.Services;
using Moq;

namespace DeployCmsData.Test.Services
{
    internal class UpgradeScriptSetup
    {
        readonly UpgradeScriptManager ScriptManager;
        public Mock<IUpgradeScript> UpgradeScript { get; }
        public Mock<IUpgradeLogRepository> LogRepository { get; }
        public UmbracoContextBuilder umbracoContextBuilder;

        public UpgradeScriptSetup()
        {
            UpgradeScript = new Mock<IUpgradeScript>();
            LogRepository = new Mock<IUpgradeLogRepository>();
            ScriptManager = new UpgradeScriptManager(LogRepository.Object);
            umbracoContextBuilder = new UmbracoContextBuilder();
        }

        public UpgradeScriptSetup RunScriptReturnsTrue()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Returns(true);
            return this;
        }

        public UpgradeScriptSetup RunScriptReturnsFalse()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Returns(false);
            return this;
        }

        public UpgradeScriptSetup RunScriptThrowsException()
        {
            UpgradeScript.Setup(x => x.RunScript(LogRepository.Object)).Throws(new Exception());
            return this;
        }

        public UpgradeScriptSetup GetLogReturnsExistingLog()
        {
            var upgradeLog = new UpgradeLog
            {
                UpgradeScriptName = UpgradeScriptManager.GetScriptName(UpgradeScript.Object)
            };
            LogRepository.Setup(x => x.GetLog(It.IsAny<string>())).Returns(upgradeLog);

            return this;
        }

        public UpgradeScriptManager Build()
        {
            return ScriptManager;
        }

    }
}
