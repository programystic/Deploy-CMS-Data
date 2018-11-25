using NUnit.Framework;
using RestSharp;

namespace DeployCmsData.IntegrationTest.Tests
{
    [TestFixture, Explicit]
    class IntegrationTests
    {
        [Test, Order(1)]
        public void ClearTheDecks()
        {           
            Assert.IsTrue(GetResponse("ClearTheDecks"));
        }

        [Test, Order(2)]
        public void Upgrade01()
        {
            Assert.IsTrue(GetResponse("Upgrade01"));
        }

        [Test, Order(3)]
        public void Upgrade02()
        {
            Assert.IsTrue(GetResponse("Upgrade02"));
        }

        [Test, Order(4)]
        public void Upgrade04()
        {
            Assert.IsTrue(GetResponse("Upgrade04"));
        }

        public bool GetResponse(string method)
        {
            return GetResponse1(method) && GetResponse2(method);
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
