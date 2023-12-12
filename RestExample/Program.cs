using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestExample;
using RestSharp;
/*
string baseurl = "https://reqres.in/api";
var client =new RestClient(baseurl);

//GetMethod
var getUserRequest =new RestRequest("users/2",Method.Get);
var getUserResponse = client.Execute(getUserRequest);
Console.WriteLine("Get Response: \n " + getUserResponse.Content);


//postmethod
var createUserRequest = new RestRequest("users", Method.Post);
createUserRequest.AddParameter("name", "John Doe");
createUserRequest.AddParameter("job", "Engineer");

var createUserResponse = client.Execute(createUserRequest);
Console.WriteLine("Post Response: \n " + createUserResponse.Content);

//put method
var updateUserRequest = new RestRequest("users/2", Method.Put);
updateUserRequest.AddParameter("name", "Updated John");
updateUserRequest.AddParameter("job", "Software engineer");


var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("Put Response: \n " + updateUserResponse.Content);

//delete method

var deleteUserRequest = new RestRequest("users/2", Method.Delete);

var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("Delete Response: \n " + deleteUserResponse.Content);*/



//string baseurl = "https://reqres.in/api";
//var client = new RestClient(baseurl);

//GetAlluser(client);
//GetSingleUser(client);
//CreateUser(client);
//UpdateUser(client);
//DeleteUser(client);

////GET ALL
//static void GetAlluser(RestClient client)
//{
//    var getUserRequest = new RestRequest("users", Method.Get);
//    getUserRequest.AddQueryParameter("page", "1");
//    var getUserResponse = client.Execute(getUserRequest);
//    Console.WriteLine("Get all Response: \n " + getUserResponse.Content);
//}

////GET
//static void GetSingleUser(RestClient client)
//{
//    var getUserRequest = new RestRequest("users/2", Method.Get);
//    var getUserResponse = client.Execute(getUserRequest);

//    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
//    {
//        JObject? userJson = JObject.Parse(getUserResponse?.Content);

//        string? userFirstName = userJson?["data"]?["first_name"]?.ToString();
//        string? userLastName = userJson?["data"]?["last_name"]?.ToString();

//        Console.WriteLine("Get Response: "+$"user name : {userFirstName} {userLastName}");
//    }
//    else
//    {
//        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
//    }

//}

////POST
//static void CreateUser(RestClient client)
//{
//    var createUserRequest = new RestRequest("users", Method.Post);
//    createUserRequest.AddHeader("Content-Type", "application/json");
//    createUserRequest.AddJsonBody(new { name = "John Doe", job = "Software engineer" });

//    var createUserResponse = client.Execute(createUserRequest);
//    Console.WriteLine("Post Response: \n " + createUserResponse.Content);
//}

////PUT
//static void UpdateUser(RestClient client)
//{
//    var updateUserRequest = new RestRequest("users/2", Method.Put);

//    updateUserRequest.AddHeader("Content-Type", "application/json");
//    updateUserRequest.AddJsonBody(new { name = "Updated John doe", job = "Senior Software Developer" });

//    var updateUserResponse = client.Execute(updateUserRequest);
//    Console.WriteLine("Put Response: \n " + updateUserResponse.Content);
//}

////DELETE
//static void DeleteUser(RestClient client)
//{
//    var deleteUserRequest = new RestRequest("users/2", Method.Delete);

//    var deleteUserResponse = client.Execute(deleteUserRequest);
//    Console.WriteLine("Delete Response: \n " + deleteUserResponse.Content);
//}

//12-12-2023

string baseurl = "https://reqres.in/api";
var client = new RestClient(baseurl);

GetSingleUser(client);
    static void GetSingleUser(RestClient client)
{
    var getUserRequest = new RestRequest("users/2", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);

    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        var response = JsonConvert.DeserializeObject<UserDataResponse>(getUserResponse.Content);
        UserData user = response.Data;
        Console.WriteLine($"Get Response\nUser ID: {user.Id}\nUser name : {user.FirstName} {user.LastName}\nEmail: {user.Email}\nAvatar: {user.Avatar}");
    }
    else
    {
        Console.WriteLine($"Error: {getUserResponse.ErrorMessage}");
    }

}