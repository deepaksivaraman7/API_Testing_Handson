using Newtonsoft.Json;
using RestfulBooking.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooking
{
    internal class RestfulBookingTests : CoreCodes
    {
        [Test, Order(1)]
        public void CreateBooking()
        {
            test = extent.CreateTest("Create booking");
            Log.Information("CreateBooking test started");

            var request = new RestRequest("booking", Method.Post);
            request.AddHeader("Content-Type", "application/json")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(new
                {
                    firstname = "Jim",
                    lastname = "Brown",
                    totalprice = 111,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2018-01-01",
                        checkout = "2019-01-01"
                    },
                    additionalneeds = "Breakfast"
                });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var bookingId = JsonConvert.DeserializeObject<BookingIdResponse>(response.Content);
                Assert.NotNull(bookingId);
                Log.Information("Booking returned");

                var bookingData = bookingId.Booking;
                Assert.That(bookingData?.FirstName, Is.EqualTo("Jim"));
                Log.Information("First name returned as expected");

                var bookingdates = bookingData.BookingDates;
                Assert.That(bookingdates?.Checkin, Is.EqualTo("2018-01-01"));
                Log.Information("Checkin date returned as expected");

                Log.Information("CreateBooking test passed all asserts");
                test.Pass("CreateBooking test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("CreateBooking test failed");
                test.Fail("CreateBooking test failed");
            }
        }
        [Test, Order(2), TestCase(2)]
        public void GetBooking(int bookingId)
        {
            test = extent.CreateTest("Get booking");
            Log.Information("GetBooking test started");

            var request = new RestRequest("booking/" + bookingId, Method.Get)
                .AddHeader("Accept", "application/json");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var bookingData = JsonConvert.DeserializeObject<BookingDataResponse>(response.Content);

                Assert.NotNull(bookingData);
                Log.Information("Booking returned");

                Assert.NotNull(bookingData.FirstName);
                Log.Information("First name is not null");

                Log.Information("GetBooking test passed all asserts");
                test.Pass("GetBooking test passed all asserts");

            }
            catch (AssertionException)
            {
                Log.Information("GetBooking test failed");
                test.Fail("GetBooking test failed");
            }
        }
        [Test, Order(3), TestCase(2)]
        public void UpdateBooking(int bookingId)
        {
            test = extent.CreateTest("Update booking");
            Log.Information("UpdateBooking test started");
            string BearerToken = "YWRtaW46cGFzc3dvcmQxMjM=";
            string? AccessToken = Auth();
            var request = new RestRequest("booking/" + bookingId, Method.Put)
            .AddHeader("Content-Type", "application/json")
            .AddHeader("Accept", "application/json")
            .AddHeader("Authorization", $"Bearer {BearerToken}")
            .AddHeader("Cookie", $"token={AccessToken}")
            .AddJsonBody(new
            {
                firstname = "James",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var booking = JsonConvert.DeserializeObject<BookingDataResponse>(response.Content);
                Assert.That(booking?.FirstName, Is.EqualTo("James"));
                Log.Information("First name returned as expected");

                Log.Information("UpdateBooking test passed all asserts");
                test.Pass("UpdateBooking test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("UpdateBooking test failed");
                test.Fail("UpdateBooking test failed");
            }
        }


        [Test, Order(4), TestCase(25)]
        public void DeleteBooking(int bookingId)
        {
            test = extent.CreateTest("Delete booking");
            Log.Information("DeleteBooking test started");
            string BearerToken = "YWRtaW46cGFzc3dvcmQxMjM=";
            string? AccessToken = Auth();
            var request = new RestRequest("booking/" + bookingId, Method.Delete)
                .AddHeader("Content-Type", "application/json")
            .AddHeader("Accept", "application/json")
            .AddHeader("Authorization", $"Bearer {BearerToken}")
            .AddHeader("Cookie", $"token={AccessToken}");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                Log.Information("DeleteBooking test passed all asserts");
                test.Pass("DeleteBooking test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("DeleteBooking test failed");
                test.Fail("DeleteBooking test failed");
            }

        }
        public string? Auth()
        {
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json")
            .AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);
            var bookingData = JsonConvert.DeserializeObject<Authentication>(response.Content);
            return bookingData?.Token;
        }

        [Test, Order(6)]
        public void GetBookingIds()
        {
            test = extent.CreateTest("Get all Booking Ids");
            Log.Information("GetBookingIds test started");

            var request = new RestRequest("booking", Method.Get)
            .AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                Assert.NotNull(response.Content);
                Log.Information("Booking Ids returned");

                Log.Information("GetBookingIds test passed all asserts");
                test.Pass("GetBookingIds test passed all asserts");
            }
            catch (AssertionException)
            {
                Log.Information("GetBookingIds test failed");
                test.Fail("GetBookingIds test failed");
            }
        }

    }
}
