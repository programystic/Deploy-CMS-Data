using NUnit.Framework;
using RestSharp;
using System;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.IntegrationTest.Tests
{
    [Explicit]
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
        [TestCase("AllDataTypes")]
        public void CallUpgradeScriptApi(string apiMethodName)
        {
            GetResponse(apiMethodName);
        }

        public void GetResponse(string method)
        {
            var endpoints = new string[] {
                "http://deploycms.umb7.0",
                "http://deploycms.umb7.4",
                "http://deploycms.umb7.6",
                "http://deploycms.umb7.13",
            };

            foreach (var endPoint in endpoints)
            {
                var client = new RestClient(endPoint);
                var request = new RestRequest($"/umbraco/api/integrationtest/runscript/?scriptName={method}", Method.GET);
                var response = client.Execute<bool>(request);

                Assert.IsTrue(response.Data, endPoint);
            }
        }
    }
}