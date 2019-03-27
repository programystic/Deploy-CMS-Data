using NUnit.Framework;
using RestSharp;
using System;
using System.Diagnostics;

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
        [TestCase("UpdateHomePage")]
        [TestCase("RemoveAllowedDocType")]   
        [TestCase("GridRowConfiguration")]
        [TestCase("DocTypeInheritance")]
        public void CallUpgradeScriptApi(string apiMethodName)
        {
            GetResponse(apiMethodName);
        }

        public void GetResponse(string method)
        {
            var endPoints = new string[] {                
                "http://deploycms.umb7.4",
                "http://deploycms.umb7.6",
                "http://deploycms.umb7.13",
                "http://deploycms.umb8.0"
            };

            foreach (var endPoint in endPoints)
            {
                var client = new RestClient(endPoint);
                var resource = $"/umbraco/api/integrationtest/runscript/?scriptName={method}";
                var request = new RestRequest(resource, Method.GET);

                Debug.WriteLine($"Calling: {endPoint}{resource}");
                var response = client.Execute<bool>(request);

                Assert.IsTrue(response.Data, endPoint);
            }
        }
    }
}