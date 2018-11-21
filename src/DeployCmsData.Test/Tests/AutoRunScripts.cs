using DeployCmsData.Test.Builders;
using DeployCmsData.Test.UpgradeScripts;
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
            var scriptManager = setup
                .AddScript(typeof(DontAutoRun))
                .AddScript(typeof(ReturnsTrue))
                .Build();
            
            var scripts = scriptManager.GetAllScripts();

            Assert.AreEqual(1, scripts.Count());
        }
    }
}
