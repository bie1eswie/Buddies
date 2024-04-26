// See https://aka.ms/new-console-template for more information
using Buddies;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");
List<MovieResult> movieResults = new List<MovieResult>();

FETCH:
int page = 1;
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
var httpClient = new HttpClient(clientHandler);
var response = await httpClient.GetAsync($"https://swapi.dev/api/people/?page={page}");

if (response.IsSuccessStatusCode)
{
    var result = await response.Content.ReadAsStringAsync();
    ResultObject movieResult = JsonConvert.DeserializeObject<ResultObject>(result)!;
    List<string> results = new List<string>();
    
    if(movieResult != null)
    {
        if(movieResult.Results!=null && movieResult.Results.Any())
        {
            movieResults.AddRange(movieResult.Results);
        }
    }
    while (movieResult!=null && string.IsNullOrEmpty(movieResult.Next))
    {
        page++;
        goto FETCH;
    }
}





