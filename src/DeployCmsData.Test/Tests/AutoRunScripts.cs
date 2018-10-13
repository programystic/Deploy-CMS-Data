using DeployCmsData.Test.Services;
using NUnit.Framework;
using System.Linq;

namespace DeployCmsData.Test.Tests
{
    class AutoRunScripts
    {
        [Test]
        public static void FindAllScripts()
        {
            var setup = new UpgradeScriptSetup();
            var scriptManager = setup.Build();

            var scripts = scriptManager.GetAllScripts();

            Assert.AreEqual(nameof(UpgradeScripts.Upgrade01), scripts.First().GetType().Name);
            Assert.AreEqual(nameof(UpgradeScripts.Upgrade02), scripts.Skip(1).First().GetType().Name);
            Assert.AreEqual(nameof(UpgradeScripts.Upgrade03), scripts.Skip(2).First().GetType().Name);            
        }
    }
}
