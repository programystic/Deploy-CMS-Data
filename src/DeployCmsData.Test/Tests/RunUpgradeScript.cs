using System;
using System.Linq;
using DeployCmsData.Core.Constants;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Core.Models;
using DeployCmsData.Core.Services;
using DeployCmsData.UnitTest.Builders;
using DeployCmsData.UnitTest.UpgradeScripts;
using Moq;
using NUnit.Framework;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.UnitTest.Tests
{
    public static class RunUpgradeScript
    {

        [Test]
        public static void GetAllScripts()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .AddScript(typeof(DoNotAutoRun))
                .AddScript(typeof(ReturnsTrue))
                .Build();

            System.Collections.Generic.IEnumerable<IUpgradeScript> scripts = scriptManager.GetAllScripts();

            Assert.AreEqual(1, scripts.Count());
        }

        [Test]
        public static void UpgradeScriptRunSuccess()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .Build();

            ReturnsTrue upgradeScript = new ReturnsTrue();
            IUpgradeLog log = scriptManager.RunScriptIfNeeded(upgradeScript);

            Assert.IsTrue(log.Success);
            Assert.IsTrue(string.IsNullOrEmpty(log.Exception));
        }

        [Test]
        public static void UpgradeScriptRunFail()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .Build();

            ReturnsFalse upgradeScript = new ReturnsFalse();
            IUpgradeLog log = scriptManager.RunScriptIfNeeded(upgradeScript);

            Assert.IsFalse(log.Success);
        }

        [Test]
        public static void UpgradeScriptFails()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .AddScript(typeof(Fails))
                .Build();

            int count = scriptManager.RunAllScriptsIfNeeded();

            Assert.Zero(count);
        }

        [Test]
        public static void UpgradeScriptRunNullScript()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .Build();

            IUpgradeLog log = scriptManager.RunScriptIfNeeded(null);

            Assert.IsFalse(log.Success);
            Assert.AreEqual(ExceptionMessages.UpgradeScriptIsNull, log.Exception);
        }

        [Test]
        public static void UpgradeScriptWriteToLogOnSuccess()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup.Build();

            ReturnsTrue upgradeScript = new ReturnsTrue();
            string scriptName = UpgradeScriptManager.GetScriptName(upgradeScript);

            IUpgradeLog log = scriptManager.RunScriptIfNeeded(upgradeScript);

            setup.LogRepository.Verify(x => x.SaveLog(It.IsAny<UpgradeLog>()));
            Assert.AreEqual(scriptName, log.UpgradeScriptName);
            Assert.IsTrue(log.Success);
            Assert.AreNotEqual(Guid.Empty, log.Id);
        }

        [Test]
        public static void UpgradeScriptWriteToLogOnFail()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup.Build();

            ReturnsFalse upgradeScript = new ReturnsFalse();
            string scriptName = UpgradeScriptManager.GetScriptName(upgradeScript);

            IUpgradeLog log = scriptManager.RunScriptIfNeeded(upgradeScript);

            setup.LogRepository.Verify(x => x.SaveLog(It.IsAny<UpgradeLog>()));
            Assert.AreEqual(scriptName, log.UpgradeScriptName);
            Assert.IsFalse(log.Success);
            Assert.AreNotEqual(Guid.Empty, log.Id);
        }

        [Test]
        public static void UpgradeScriptAlreadyRun()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .GetLogsReturnsSuccessfulLogs()
                .AddScript(typeof(ReturnsTrue))
                .AddScript(typeof(ReturnsTrue))
                .AddScript(typeof(ReturnsTrue))
                .Build();

            int count = scriptManager.RunAllScriptsIfNeeded();

            Assert.Zero(count);
        }

        [Test]
        public static void RerunUpgradeScript()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .GetLogsReturnsSuccessfulLogs()
                .Build();

            ReturnsTrue upgradeScript = new ReturnsTrue();
            UpgradeLog log = scriptManager.RunScript(upgradeScript);

            Assert.IsTrue(log.Success);
        }

        [Test]
        public static void RunScriptEveryTime()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .GetLogsReturnsSuccessfulLogs()
                .Build();

            RunEveryTime upgradeScript = new RunEveryTime();
            IUpgradeLog log = scriptManager.RunScriptIfNeeded(upgradeScript);

            Assert.IsTrue(log.Success);
        }

        [Test]
        public static void DoNotAutoRunScript()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .AddScript(typeof(DoNotAutoRun))
                .AddScript(typeof(ReturnsTrue))
                .AddScript(typeof(RunEveryTime))
                .Build();

            int count = scriptManager.RunAllScriptsIfNeeded();

            Assert.AreEqual(2, count);
        }

        [Test]
        public static void RunScriptTakesABitOfTime()
        {
            UpgradeScriptManagerBuilder setup = new UpgradeScriptManagerBuilder();
            UpgradeScriptManager scriptManager = setup
                .Build();

            Sleeps script = new Sleeps();
            IUpgradeLog log = scriptManager.RunScriptIfNeeded(script);

            Assert.Greater(log.RuntTimeMilliseconds, 0);
        }
    }
}