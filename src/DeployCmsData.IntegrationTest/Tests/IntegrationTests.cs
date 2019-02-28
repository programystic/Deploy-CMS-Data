using NUnit.Framework;
using RestSharp;
using System;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.IntegrationTest.Tests
{
    internal class IntegrationTests
    {
        [TestCase("ClearTheDecks")]
        [TestCase("Upgrade01")]
        [TestCase("Upgrade02")]
        [TestCase("Upgrade04")]
        [TestCase("BuildHomepage")]
        [TestCase("BuildWebsite")]
        [TestCase("Templates")]
        [TestCase("CreateContent")]
        [TestCase("MultiNodeTreePicker")]
        [TestCase("BusinessCase01")]
        [TestCase("AllDataTypes")]
        public void CallUpgradeScriptApi(string apiMethodName)
        {
            Assert.IsTrue(GetResponse(apiMethodName));
        }

        public bool GetResponse(string method)
        {
            var endpoints = new string[] { "http://deploycms.umb7.4" };

            foreach (var endPoint in endpoints)
            {
                var client = new RestClient(endPoint);
                var request = new RestRequest($"/umbraco/api/integrationtest/runscript/?scriptName={method}", Method.GET);
                var response = client.Execute<bool>(request);

                if (!response.Data)
                {
                    return false;
                }
            }

            return true;
            //client.Authenticator = new HttpBasicAuthenticator("peter@programystic.com", "tennesee55");
        }
    }
}