using NUnit.Framework;
using RestSharp;

namespace DeployCmsData.IntegrationTest.Tests
{
    class IntegrationTests
    {
        [Test]
        public void ClearTheDecks()
        {
            var client = new RestClient("http://example.com");
        }
    }
}
