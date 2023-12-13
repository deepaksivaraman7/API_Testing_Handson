using AssignmentsNUnit.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;


namespace AssignmentsNUnit
{
    internal class JsonPlaceHolderTests:CoreCodes
    {
        [Test, Order(2),TestCase(2)]
        public void GetSinglePost(int postId)
        {
            test = extent.CreateTest("Get single post");
            Log.Information("GetSinglePost test started");

            var request = new RestRequest("posts/"+postId, Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var post = JsonConvert.DeserializeObject<PostData>(response.Content);
                Assert.NotNull(post);
                Log.Information("Post returned");

                Assert.That(post.Id, Is.EqualTo(postId));
                Log.Information("Post ID matches with fetch");

                Assert.IsNotEmpty(post.Title);
                Log.Information("Title is not empty");

                Log.Information("GetSinglePost test passed all asserts");
                test.Pass("GetSinglePost test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("GetSinglePost test failed");
                test.Fail("GetSinglePost test failed");
            }
        }
        [Test, Order(1)]
        public void CreatePost()
        {
            test = extent.CreateTest("Create post");
            Log.Information("CreatePost test started");

            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 100, title="new title", body="new body" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var post = JsonConvert.DeserializeObject<PostData>(response.Content);

                Assert.NotNull(post);
                Log.Information("Post returned");

                Assert.NotNull(post.Id);
                Log.Information("Post ID is not null");

                Log.Information("CreatePost test passed all asserts");
                test.Pass("CreatePost test passed all asserts");
            }
            catch(AssertionException)
            {
                Log.Information("CreatePost test failed");
                test.Fail("CreatePost test failed");
            }
        }
        [Test, Order(3),TestCase(2)]
        public void UpdatePost(int postId)
        {
            test = extent.CreateTest("Update post");
            Log.Information("UpdatePost test started");

            var request = new RestRequest("posts/"+postId, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 100, title = "new title", body = "new body" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var post = JsonConvert.DeserializeObject<PostData>(response.Content);

                Assert.NotNull(post);
                Log.Information("Post returned");

                Assert.That(post.Id, Is.EqualTo(postId));
                Log.Information("Post ID matches with fetch");

                Log.Information("UpdatePost test passed all asserts");
                test.Pass("UpdatePost test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("UpdatePost test failed");
                test.Fail("UpdatePost test failed");
            }
        }
        [Test, Order(4),TestCase(2)]
        public void DeletePost(int postId)
        {
            test = extent.CreateTest("Delete post");
            Log.Information("DeletePost test started");

            var request = new RestRequest("posts/"+postId, Method.Delete);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                Assert.That(response.Content,Is.EqualTo("{}"));
                Log.Information("Empty response returned");

                Log.Information("DeletePost test passed all asserts");
                test.Pass("DeletePost test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("DeletePost test failed");
                test.Fail("DeletePost test failed");
            }
        }
        [Test, Order(5),TestCase(999)]
        public void GetNonExistingPost(int postId)
        {
            test = extent.CreateTest("Get non existing post");
            Log.Information("GetNonExistingPost test started");

            var request = new RestRequest("posts/"+postId, Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information($"API Response: {response.Content}");

                Log.Information("GetNonExistingPost test passed all asserts");
                test.Pass("GetNonExistingPost test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("GetNonExistingPost test failed");
                test.Fail("GetNonExistingPost test failed");
            }
        }
        [Test, Order(6),TestCase(1)]
        public void GetAllPosts(int pageNo)
        {
            test = extent.CreateTest("Get all posts");
            Log.Information("GetAllPosts test started");

            var request = new RestRequest("posts", Method.Get);
            request.AddQueryParameter("page", pageNo);
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                Assert.NotNull(response.Content);
                Log.Information("Posts returned");

                Log.Information("GetAllPosts test passed all asserts");
                test.Pass("GetAllPosts test passed all asserts");
            }
            catch(AssertionException)
            {
                Log.Information("GetAllPosts test failed");
                test.Fail("GetAllPosts test failed");
            }
        }
    }
}
