using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExampleNUnit
{
    [TestFixture]
    internal class ReqResApiTests
    {
        private RestClient client;
        private string baseUrl = "https://reqres.in/api";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }

        [Test]
        public void GetSingleUser()
        {
            var request = new RestRequest("users/2", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var userData = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
            UserData? user = userData?.Data;

            Assert.NotNull(user);
            Assert.That(user.Id,Is.EqualTo(2));
            Assert.IsNotEmpty(user.Email);
        }
    }
}
