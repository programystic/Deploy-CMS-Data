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
        string[] _endPoints = new string[] {
                "http://deploycms.umb7.4",
                "http://deploycms.umb7.6",
                "http://deploycms.umb7.8",
                "http://deploycms.umb7.13"
                //,"http://deploycms.umb8.0"
            };

        [OneTimeSetUp]
        public void Setup()
        {
            foreach (var endpoint in _endPoints)
            {
                RestExecute(endpoint);
            }
        }

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
        //[TestCase("AllDataTypeIds")]
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
            foreach (var endPoint in _endPoints)
            {
                var resource = $"/umbraco/api/integrationtest/runscript/?scriptName={method}";
                var response = RestExecute(endPoint, resource);

                if (response.ErrorException != null)
                {
                    throw new Exception(endPoint + " : " + response.Content, response.ErrorException);
                }

                Assert.IsTrue(response.Data, endPoint + resource);
            }
        }

        public IRestResponse<bool> RestExecute(string endPoint)
        {
            return RestExecute(endPoint, null);
        }

        public IRestResponse<bool> RestExecute(string endPoint, string resource)
        {                        
            var client = new RestClient(endPoint);
            var request = new RestRequest(resource, Method.GET);

            Debug.WriteLine($"Calling: {endPoint}");
            return client.Execute<bool>(request);
        }
    }
}