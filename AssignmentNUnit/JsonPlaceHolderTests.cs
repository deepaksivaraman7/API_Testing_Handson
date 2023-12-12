using Newtonsoft.Json;
using RestSharp;


namespace AssignmentsNUnit
{
    internal class JsonPlaceHolderTests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }

        [Test, Order(2)]
        public void GetSinglePost()
        {
            var request = new RestRequest("posts/2", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var post = JsonConvert.DeserializeObject<PostData>(response.Content);

            Assert.NotNull(post);
            Assert.That(post.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(post.Title);
        }
        [Test, Order(1)]
        public void CreatePost()
        {
            var request = new RestRequest("users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 100, title="new title", body="new body" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var post = JsonConvert.DeserializeObject<PostData>(response.Content);

            Assert.NotNull(post);
            Assert.NotNull(post.Id);
        }
        [Test, Order(3)]
        public void UpdatePost()
        {
            var request = new RestRequest("posts/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 100, title = "new title", body = "new body" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var post = JsonConvert.DeserializeObject<PostData>(response.Content);

            Assert.NotNull(post);
            Assert.That(post.Id,Is.EqualTo(2));
        }
        [Test, Order(4)]
        public void DeletePost()
        {
            var request = new RestRequest("posts/2", Method.Delete);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(response.Content,Is.EqualTo("{}"));
        }
        [Test, Order(5)]
        public void GetNonExistingPost()
        {
            var request = new RestRequest("posts/999", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
        [Test, Order(6)]
        public void GetAllPosts()
        {
            var request = new RestRequest("users", Method.Get);
            request.AddQueryParameter("page", "1");
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.NotNull(response.Content);
            Console.WriteLine(response.Content);
        }
    }
}
