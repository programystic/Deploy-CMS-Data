using System;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using DeployCmsData.Services;
using Moq;
using NUnit.Framework;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    public static class UpgradeScriptTests
    {
        [Test]
        public static void UpgradeScriptRunSuccess()
        {
            var upgradeScript = new Mock<IUpgradeScript>();
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);
            upgradeScript.Setup(x => x.RunScript(logRepository.Object)).Returns(true);

            var log = scriptManager.RunScript(upgradeScript.Object);
            
            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.IsTrue(log.Success);
            Assert.IsTrue(string.IsNullOrEmpty(log.Exception));
        }

        [Test]
        public static void UpgradeScriptRunFail()
        {
            var upgradeScript = new Mock<IUpgradeScript>();
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);
            upgradeScript.Setup(x => x.RunScript(logRepository.Object)).Returns(false);

            var log = scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.IsFalse(log.Success);            
        }

        [Test]
        public static void UpgradeScriptRaisesException()
        {
            var upgradeScript = new Mock<IUpgradeScript>();
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);

            upgradeScript.Setup(x => x.RunScript(logRepository.Object)).Throws(new Exception());

            var log = scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
            Assert.IsFalse(log.Success);
            Assert.IsFalse(string.IsNullOrEmpty(log.Exception));
        }

        [Test]
        public static void UpgradeScriptRunNullScript()
        {
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);

            var log = scriptManager.RunScript(null);
            
            Assert.IsFalse(log.Success);
            Assert.AreEqual(ExceptionMessages.UpgradeScriptIsNull, log.Exception);
        }

        [Test]
        public static void UpgradeScriptWriteToLog()
        {            
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);

            var upgradeScript = new Mock<IUpgradeScript>();
            upgradeScript.Setup(x => x.RunScript(logRepository.Object)).Returns(true);
            var scriptName = upgradeScript.Object.GetType().FullName;

            var upgradeLog = scriptManager.RunScript(upgradeScript.Object);

            logRepository.Verify(x => x.SaveLog(It.IsAny<UpgradeLog>()));
            Assert.AreEqual(scriptName, upgradeLog.UpgradeScriptName);
            Assert.IsTrue(upgradeLog.Success);
            Assert.AreNotEqual(Guid.Empty, upgradeLog.Id);            
        }

        [Test]
        public static void UpgradeScriptAlreadyRun()
        {
            var upgradeScript = new Mock<IUpgradeScript>();
            var scriptName = upgradeScript.Object.GetType().FullName;
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);
            var upgradeLog = new UpgradeLog() {UpgradeScriptName = scriptName};
            logRepository.Setup(x => x.GetLog(scriptName)).Returns(upgradeLog);

            scriptManager.RunScript(upgradeScript.Object);
            
            upgradeScript.Verify(x => x.RunScript(logRepository.Object), Times.Never);
        }

        [Test]
        public static void UpgradeScriptNotAlreadyRun()
        {
            var upgradeScript = new Mock<IUpgradeScript>();
            var scriptName = upgradeScript.GetType().Name;
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);
            logRepository.Setup(x => x.GetLog(scriptName)).Returns((UpgradeLog)null);

            scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(logRepository.Object), Times.Once);
        }
    }
}
