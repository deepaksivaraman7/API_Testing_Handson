using Newtonsoft.Json.Linq;
using RestSharp;

string baseurl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseurl);


GetAlluser(client);
GetSingleUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);

//GET ALL
static void GetAlluser(RestClient client)
{

    var getUserRequest = new RestRequest("posts", Method.Get);
    getUserRequest.AddQueryParameter("page", "1");
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("Get all Response: \n " + getUserResponse.Content);
}

//GET
static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/1", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);

    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {

        JObject? userJson = JObject.Parse(getUserResponse?.Content);

        string? userId = userJson?["data"]?["id"]?.ToString();
        string? userTitle = userJson?["data"]?["title"]?.ToString();

        Console.WriteLine($"User Details  : {userId} {userTitle}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }

}

//POST
static void CreateUser(RestClient client)
{

    //postmethod
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new
    {
        userId = "43",
        id = "43",
        title = "Home",
        body = "architecto"
    });


    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("Post Response: \n " + createUserResponse.Content);
}

//PUT
static void UpdateUser(RestClient client)
{
    //put method
    var updateUserRequest = new RestRequest("posts/1", Method.Put);



    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new
    {
        userId = "143",
        id = "43",
        title = "Homes",
        body = "architecto"
    });


    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("Put Response: \n " + updateUserResponse.Content);
}

//DELETE
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/1", Method.Delete);

    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("Delete Response: \n " + deleteUserResponse.Content);
}