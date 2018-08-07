using System;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using DeployCmsData.Services;
using DeployCmsData.Test.Services;
using Moq;
using NUnit.Framework;

namespace DeployCmsData.Test.Tests
{
    public static class RunUpgradeScript
    {
        [Test]
        public static void UpgradeScriptRunSuccess()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .RunScriptReturnsTrue()
                .Build();

            var upgradeScript = setup.UpgradeScript;
            var log = scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.IsTrue(log.Success);
            Assert.IsTrue(string.IsNullOrEmpty(log.Exception));
        }

        [Test]
        public static void UpgradeScriptRunFail()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .RunScriptReturnsFalse()
                .Build();

            var upgradeScript = setup.UpgradeScript;
            var log = scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.IsFalse(log.Success);            
        }

        [Test]
        public static void UpgradeScriptRaisesException()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .RunScriptThrowsException()
                .Build();

            var upgradeScript = setup.UpgradeScript;
            var log = scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.IsFalse(log.Success);
            Assert.IsFalse(string.IsNullOrEmpty(log.Exception));
        }

        [Test]
        public static void UpgradeScriptRunNullScript()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .Build();

            var log = scriptManager.RunScript(null);

            Assert.IsFalse(log.Success);
            Assert.AreEqual(ExceptionMessages.UpgradeScriptIsNull, log.Exception);
        }

        [Test]
        public static void UpgradeScriptWriteToLog()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .RunScriptReturnsTrue()
                .Build();

            var upgradeScript = setup.UpgradeScript;
            var scriptName = scriptManager.GetScriptName(upgradeScript.Object);

            var log = scriptManager.RunScript(upgradeScript.Object);

            setup.LogRepository.Verify(x => x.SaveLog(It.IsAny<UpgradeLog>()));
            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.AreEqual(scriptName, log.UpgradeScriptName);
            Assert.IsTrue(log.Success);
            Assert.AreNotEqual(Guid.Empty, log.Id);            
        }

        [Test]
        public static void UpgradeScriptAlreadyRun()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .GetLogReturnsExistingLog()
                .Build();

            var upgradeScript = setup.UpgradeScript;
            scriptManager.RunScript(upgradeScript.Object);
            
            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Never);
        }

        [Test]
        public static void ReRunUpgradeScript()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup
                .GetLogReturnsExistingLog()
                .Build();

            var upgradeScript = setup.UpgradeScript;
            scriptManager.RunScriptAgain(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
        }
    }
}
