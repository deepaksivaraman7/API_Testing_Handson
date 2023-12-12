using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExample
{
    public class APIWithExceptions
    {
        string baseUrl = "https://reqres.in/api/";
        public void GetSingleUser()
        {
            var client = new RestClient(baseUrl);
            var req = new RestRequest("users/5", Method.Get);
            var response = client.Execute(req);

            //with error
            if (!response.IsSuccessful)
            {
                try
                {
                    var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    if (errorDetails != null)
                    {
                        Console.WriteLine($"API Error: {errorDetails.Error}");
                    }
                }
                catch(JsonException)
                {
                    Console.WriteLine("Failed to deserialize error response");
                }
            }
        }
    }
}
