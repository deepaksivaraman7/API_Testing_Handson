using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using RestExampleNUnit.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExampleNUnit
{
    internal class ReqResTests : CoreCodes
    {

        [Test, Order(2),TestCase(2)]
        public void GetSingleUser(int userId)
        {
            test = extent.CreateTest("Get single user");
            Log.Information("GetSingleUser test started");

            var request = new RestRequest("users/"+userId, Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var userData = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                UserData? user = userData?.Data;

                Assert.NotNull(user);
                Log.Information("User returned");

                Assert.That(user.Id, Is.EqualTo(userId));
                Log.Information("User ID matches with fetch");

                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");

                Log.Information("GetSingleUser test passed all asserts");
                test.Pass("GetSingleUser test passed all asserts");

            }
            catch (AssertionException)
            {
                Log.Information("GetSingleUser test failed");
                test.Fail("GetSingleUser test failed");
            }
        }
        [Test, Order(6)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("Get all users");
            Log.Information("GetAllUsers test started");

            var request = new RestRequest("users", Method.Get);
            request.AddQueryParameter("page", "1");
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                Assert.NotNull(response.Content);
                Log.Information("Users returned");

                Log.Information("GetAllUsers test passed all asserts");
                test.Pass("GetAllUsers test passed all asserts");
            }
            catch(AssertionException)
            {
                Log.Information("GetAllUsers test failed");
                test.Fail("GetAllUsers test failed");
            }
        }
        [Test, Order(1)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("CreateUser test started");

            var request = new RestRequest("users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User returned");

                Assert.IsNull(user.Email);
                Log.Information("User email is null as expected");

                Log.Information("CreateUser test passed all asserts");
                test.Pass("CreateUser test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("CreateUser test failed");
                test.Fail("CreateUser test failed");
            }
        }
        [Test, Order(3), TestCase(2)]
        public void UpdateUser(int userId)
        {
            test = extent.CreateTest("Update user");
            Log.Information("UpdateUser test started");

            var request = new RestRequest("users/"+userId, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Updated John Doe", job = "Senior Software Developer" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User returned");

                Assert.IsNull(user.Email);
                Log.Information("User email is null as expected");

                Log.Information("UpdateUser test passed all asserts");
                test.Pass("UpdateUser test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("UpdateUser test failed");
                test.Fail("UpdateUser test failed");
            }
        }
        [Test, Order(4), TestCase(2)]
        public void DeleteUser(int userId)
        {
            test = extent.CreateTest("Delete user");
            Log.Information("DeleteUser test started");

            var request = new RestRequest("users/"+userId, Method.Delete);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information($"API Response: {response.Content}");

                Log.Information("DeleteUser test passed all asserts");
                test.Pass("DeleteUser test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("DeleteUser test failed");
                test.Fail("DeleteUser test failed");
            }

        }
        [Test, Order(5), TestCase(999)]
        public void GetNonExistingUser(int userId)
        {
            test = extent.CreateTest("Get non existing user");
            Log.Information("GetNonExistingUser test started");

            var request = new RestRequest("users/"+userId, Method.Get);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information($"API Response: {response.Content}");

                Log.Information("GetNonExistingUser test passed all asserts");
                test.Pass("GetNonExistingUser test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("GetNonExistingUser test failed");
                test.Pass("GetNonExistingUser test failed");
            }
        }
    }
}
