using NUnit.Framework;
using RestSharp;

namespace DeployCmsData.IntegrationTest.Tests
{
    [TestFixture, Explicit]
    class IntegrationTests
    {
        [TestCase("ClearTheDecks")]
        [TestCase("Upgrade01")]
        [TestCase("Upgrade02")]
        [TestCase("Upgrade04")]        
        [TestCase("BuildHomePage")]
        [TestCase("BuildWebsite")]
        [TestCase("CreateContent")]
        public void CallUpgradeScriptApi(string apiMethodName)
        {
            Assert.IsTrue(GetResponse1(apiMethodName));
            Assert.IsTrue(GetResponse2(apiMethodName));
        }

        public bool GetResponse1(string method)
        {
            var client = new RestClient("http://deploycms.sqlce");
            var request = new RestRequest($"/umbraco/api/tests/runscript/?scriptName={method}", Method.GET);

            var response = client.Execute<bool>(request);

            return response.Data;
        }

        public bool GetResponse2(string method)
        {
            var client = new RestClient("http://deploycms.sqlserver");
            var request = new RestRequest($"/umbraco/api/tests/runscript/?scriptName={method}", Method.GET);

            var response = client.Execute<bool>(request);

            return response.Data;
        }
    }
}
