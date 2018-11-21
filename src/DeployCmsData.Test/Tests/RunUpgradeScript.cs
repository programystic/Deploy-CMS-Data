using DeployCmsData.Core.Constants;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using DeployCmsData.Core.Services;
using DeployCmsData.Test.Builders;
using DeployCmsData.Test.UpgradeScripts;
using Moq;
using NUnit.Framework;
using System;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Test.Tests
{
    public static class RunUpgradeScript
    {
        [Test]
        public static void UpgradeScriptRunSuccess()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .RunScriptReturnsTrue()
                .Build();

            var upgradeScript = new ReturnsTrue();
            var log = scriptManager.RunScriptIfNeeded(upgradeScript);
            
            Assert.IsTrue(log.Success);
            Assert.IsTrue(string.IsNullOrEmpty(log.Exception));
        }

        [Test]
        public static void UpgradeScriptRunFail()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup                
                .Build();

            var upgradeScript = new ReturnsFalse();
            var log = scriptManager.RunScriptIfNeeded(upgradeScript);
            
            Assert.IsFalse(log.Success);            
        }

        [Test]
        public static void UpgradeScriptRaisesException()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .AddScript(typeof(ThrowsException))
                .Build();

            var count = scriptManager.RunAllScriptsIfNeeded();

            Assert.Zero(count);
        }

        [Test]
        public static void UpgradeScriptRunNullScript()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .Build();

            var log = scriptManager.RunScriptIfNeeded(null);

            Assert.IsFalse(log.Success);
            Assert.AreEqual(ExceptionMessages.UpgradeScriptIsNull, log.Exception);
        }

        [Test]
        public static void UpgradeScriptWriteToLogOnSuccess()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .Build();

            var upgradeScript = new ReturnsTrue();
            var scriptName = UpgradeScriptManager.GetScriptName(upgradeScript);

            var log = scriptManager.RunScriptIfNeeded(upgradeScript);

            setup.LogRepository.Verify(x => x.SaveLog(It.IsAny<UpgradeLog>()));            
            Assert.AreEqual(scriptName, log.UpgradeScriptName);
            Assert.IsTrue(log.Success);
            Assert.AreNotEqual(Guid.Empty, log.Id);            
        }

        [Test]
        public static void UpgradeScriptWriteToLogOnFail()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .RunScriptReturnsTrue()
                .Build();

            var upgradeScript = new ReturnsFalse();
            var scriptName = UpgradeScriptManager.GetScriptName(upgradeScript);

            var log = scriptManager.RunScriptIfNeeded(upgradeScript);

            setup.LogRepository.Verify(x => x.SaveLog(It.IsAny<UpgradeLog>()));
            Assert.AreEqual(scriptName, log.UpgradeScriptName);
            Assert.IsFalse(log.Success);
            Assert.AreNotEqual(Guid.Empty, log.Id);
        }

        [Test]
        public static void UpgradeScriptAlreadyRun()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .GetLogsReturnsSuccessfulLogs()
                .AddScript(typeof(ReturnsTrue))
                .AddScript(typeof(ReturnsTrue))
                .AddScript(typeof(ReturnsTrue))
                .Build();
            
            var count = scriptManager.RunAllScriptsIfNeeded();

            Assert.Zero(count);
        }

        [Test]
        public static void ReRunUpgradeScript()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .GetLogsReturnsSuccessfulLogs()
                .Build();

            var upgradeScript = new ReturnsTrue();
            var log = scriptManager.RunScript(upgradeScript);

            Assert.IsTrue(log.Success);
        }

        [Test]
        public static void ScriptHasRunScriptEveryTimeAttribute()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .GetLogsReturnsSuccessfulLogs()                
                .Build();

            var upgradeScript = new RunEveryTime();
            var log = scriptManager.RunScriptIfNeeded(upgradeScript);

            Assert.IsTrue(log.Success);
        }

        [Test]
        public static void DontAutoRunScript()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup                
                .AddScript(typeof(DontAutoRun))
                .AddScript(typeof(ReturnsTrue))
                .AddScript(typeof(RunEveryTime))
                .Build();
            
            var count = scriptManager.RunAllScriptsIfNeeded();

            Assert.AreEqual(2, count);
        }

        [Test]
        public static void ScriptTakesSomeTimeToRun()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup
                .Build();

            var script = new Sleeps();            
            var log = scriptManager.RunScriptIfNeeded(script);

            Assert.Greater(log.RuntTimeMilliseconds, 0);
        }
    }
}
