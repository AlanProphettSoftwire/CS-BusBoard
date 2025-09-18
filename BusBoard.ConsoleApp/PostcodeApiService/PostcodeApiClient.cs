using BusBoard.ConsoleApp.TflModels;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serializers.Json;

namespace BusBoard.ConsoleApp.PostcodeApiService;

public class PostcodeApiClient
{
    private readonly RestClient _client;
    
    public PostcodeApiClient()
    {
        RestClientOptions options = new RestClientOptions("https://api.postcodes.io/postcodes/");
        _client = new RestClient(
            options,
            configureSerialization: s => s.UseSystemTextJson());
    }

    public PostcodeResponseModel GetLonLatFromPostcode(string postcode)
    {
        var request = new RestRequest(postcode);
        var response = _client.Execute<PostcodeResponseModel>(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception($"Something went wrong." +
                                $"status code: '{response.StatusCode}' on Postcode API request for: '{postcode}'"); 
        }
        
        if (response.Data == null)
        {
            throw new Exception($"Something went wrong. on Postcode API request for: '{postcode}'" +
                                $"status code: '{response.StatusCode}' but data is null. Failed to deserialize."); 
        }

        PostcodeResponseModel responseRecords = response.Data;

        return responseRecords;
    }
}