using System;
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
        public static void UpgradeScriptRun()
        {
            var upgradeScript = new Mock<IUpgradeScript>();
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);
            
            scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(It.IsAny<IUpgradeLogRepository>()), Times.Once);
        }

        [Test]
        public static void UpgradeScriptRunNullScript()
        {
            var logRepository = new Mock<IUpgradeLogRepository>();
            var scriptManager = new UpgradeScriptManager(logRepository.Object);

            Assert.Throws<ArgumentNullException>(() => scriptManager.RunScript(It.IsAny<IUpgradeScript>()));            
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
            logRepository.Setup(x => x.GetLog(scriptName)).Returns((UpgradeLog) null);

            scriptManager.RunScript(upgradeScript.Object);

            upgradeScript.Verify(x => x.RunScript(logRepository.Object), Times.Once);
        }
    }
}
