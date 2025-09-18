using RestSharp;
using RestSharp.Serializers.Json;
using Microsoft.Extensions.Configuration;

namespace BusBoard.ConsoleApp.TflApiService;

public class TflApiClient
{
    private readonly RestClient _client;
    
    public TflApiClient()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();
        
        RestClientOptions options = new RestClientOptions("https://api.tfl.gov.uk/");
        _client = new RestClient(
            options,
            configureSerialization: s => s.UseSystemTextJson());

        string tflSecretKey = config["tflApi:key"];
        if (string.IsNullOrWhiteSpace(tflSecretKey))
        {
            throw new Exception("Tfl API key not configured");
        }
        
        _client.AddDefaultParameter("app_key", tflSecretKey);
    }

    public T GetApiResponse<T>(string resource)
    {
        return this.GetApiResponse<T>(resource, []);
    }

    
    public T GetApiResponse<T>(string resource, List<Tuple<string, string>> queryOptions)
    {
        var request = new RestRequest(resource);
        
        foreach (var queryOption in queryOptions)
        {
            request.AddParameter(queryOption.Item1, queryOption.Item2);
        }
        
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