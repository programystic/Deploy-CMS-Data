using NUnit.Framework;
using RestSharp;
using System;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.IntegrationTest.Tests
{
    [TestFixture, Explicit]
    internal class IntegrationTests
    {
        [TestCase("ClearTheDecks")]
        [TestCase("Upgrade01")]
        [TestCase("Upgrade02")]
        [TestCase("Upgrade04")]
        [TestCase("BuildHomePage")]
        [TestCase("BuildWebsite")]
        [TestCase("CreateContent")]
        [TestCase("MultiNodeTreePicker")]
        [TestCase("BusinessCase01")]
        [TestCase("AllDataTypes")]
        public void CallUpgradeScriptApi(string apiMethodName)
        {
            Assert.IsTrue(GetResponse2(apiMethodName));
            Assert.IsTrue(GetResponse1(apiMethodName));
        }

        public bool GetResponse1(string method)
        {
            var client = new RestClient("http://deploycms.sqlce");
            //client.Authenticator = new HttpBasicAuthenticator("peter@programystic.com", "tennesee55");

            var request = new RestRequest($"/umbraco/api/tests/runscript/?scriptName={method}", Method.GET);
            var response = client.Execute<bool>(request);

            return response.Data;
        }

        public static bool GetResponse2(string method)
        {
            var client = new RestClient("http://deploycms.sqlserver");
            //client.Authenticator = new HttpBasicAuthenticator("peter@programystic.com", "tennesee55");

            var request = new RestRequest($"/umbraco/api/tests/runscript/?scriptName={method}", Method.GET);
            var response = client.Execute<bool>(request);

            return response.Data;
        }
    }
}
