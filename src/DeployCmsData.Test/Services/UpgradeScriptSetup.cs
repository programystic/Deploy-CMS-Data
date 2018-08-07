using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using DeployCmsData.Services;
using Moq;

namespace DeployCmsData.Test.Services
{
    internal class UpgradeScriptSetup
    {
        private UpgradeScriptManager _scriptManager;
        public Mock<IUpgradeScript> UpgradeScript { get; }
        public Mock<IUpgradeLogRepository> LogRepository { get; }

        public UpgradeScriptSetup()
        {
            UpgradeScript = new Mock<IUpgradeScript>();
            LogRepository = new Mock<IUpgradeLogRepository>();
            _scriptManager = new UpgradeScriptManager(LogRepository.Object);
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
                UpgradeScriptName = _scriptManager.GetScriptName(UpgradeScript.Object)
            };
            LogRepository.Setup(x => x.GetLog(It.IsAny<string>())).Returns(upgradeLog);

            return this;
        }

        public UpgradeScriptManager Build()
        {
            return _scriptManager;
        }

    }
}
