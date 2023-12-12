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

        [Test, Order(2)]
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
        [Test, Order(6)]
        public void GetAllUsers()
        {
            var request = new RestRequest("users", Method.Get);
            request.AddQueryParameter("page", "1");
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.NotNull(response.Content);
            Console.WriteLine(response.Content);
        }
        [Test,Order(1)]
        public void CreateUser()
        {
            var request = new RestRequest("users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);

            Assert.NotNull(user);
            Assert.IsNull(user.Email);
        }
        [Test, Order(3)]
        public void UpdateUser()
        {
            var request = new RestRequest("users/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Updated John Doe", job = "Senior Software Developer" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);

            Assert.NotNull(user);
            Assert.IsNull(user.Email);
        }
        [Test, Order(4)]
        public void DeleteUser()
        {
            var request = new RestRequest("users/2", Method.Delete);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
        }
        [Test, Order(5)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("users/999", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
