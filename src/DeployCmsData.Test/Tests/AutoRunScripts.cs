using DeployCmsData.Test.Builders;
using NUnit.Framework;
using System.Linq;

namespace DeployCmsData.Test.Tests
{
    class AutoRunScripts
    {
        [Test]
        public static void FindTestScripts()
        {
            var setup = new UpgradeScriptManagerBuilder();
            var scriptManager = setup.Build();

            var scripts = scriptManager.GetAllScripts();

            Assert.AreEqual(nameof(UpgradeScripts.Upgrade001Test), scripts.First().GetType().Name);
            Assert.AreEqual(nameof(UmbracoCms.UpgradeScripts.Upgrade01), scripts.Skip(1).First().GetType().Name);
            Assert.AreEqual(nameof(UmbracoCms.UpgradeScripts.Upgrade02), scripts.Skip(2).First().GetType().Name);
            Assert.AreEqual(nameof(UpgradeScripts.Upgrade02Test), scripts.Skip(3).First().GetType().Name);            
            Assert.AreEqual(nameof(UmbracoCms.UpgradeScripts.Upgrade04), scripts.Skip(5).First().GetType().Name);            
        }
    }
}
