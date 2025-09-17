using RestSharp;
using RestSharp.Serializers.Json;
using Microsoft.Extensions.Configuration;

namespace BusBoard.ConsoleApp.TflApiService;

public class TflApiClient
{
    private readonly RestClient _client;
    
    public TflApiClient(IConfiguration secretConfiguration)
    {
        RestClientOptions options = new RestClientOptions("https://api.tfl.gov.uk/");
        _client = new RestClient(
            options,
            configureSerialization: s => s.UseSystemTextJson());
        
        _client.AddDefaultParameter("app_key",secretConfiguration["tflApi:key"]);
    }
    
    public T GetApiResponse<T>(string resource)
    {
        var request = new RestRequest(resource);
        var response = _client.Execute<T>(request);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception($"Something went wrong." +
                                $"status code: '{response.StatusCode}' on TFL API request: '{resource}'"); 
        }
        
        if (response.Data == null)
        {
            throw new Exception($"Something went wrong. on TFL API request: '{resource}'" +
                                $"status code: '{response.StatusCode}' but data is null. Failed to deserialize."); 
        }

        T responseRecords = response.Data;

        return responseRecords;
    }
}