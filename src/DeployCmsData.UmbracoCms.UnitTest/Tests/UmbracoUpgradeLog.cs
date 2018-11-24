using System;
using DeployCmsData.Core.Models;
using NUnit.Framework;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    [TestFixture]
    public static class UmbracoUpgradeLog
    {
        [Test]
        public static void MapLogModel()
        {
            var log = new UpgradeLog
            {
                Id = 1,
                Exception = "My Exception",
                RuntTimeMilliseconds = 15,
                Success = true,
                Timestamp = new DateTime(2018, 10, 24, 22, 08, 15),
                UpgradeScriptName = "My Script Name"
            };

            var umbracoUpgradeLog = new UmbracoCms.Models.UmbracoUpgradeLog(log);

            Assert.AreEqual(log.Id, umbracoUpgradeLog.Id);
            Assert.AreEqual(log.Exception, umbracoUpgradeLog.Exception);
            Assert.AreEqual(log.RuntTimeMilliseconds, umbracoUpgradeLog.RuntTimeMilliseconds);
            Assert.AreEqual(log.Success, umbracoUpgradeLog.Success);
            Assert.AreEqual(log.Timestamp, umbracoUpgradeLog.Timestamp);
            Assert.AreEqual(log.UpgradeScriptName, umbracoUpgradeLog.UpgradeScriptName);
        }
    }
}
