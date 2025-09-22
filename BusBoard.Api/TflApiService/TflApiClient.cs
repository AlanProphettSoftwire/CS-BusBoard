using RestSharp;
using RestSharp.Serializers.Json;

namespace BusBoard.Api.TflApiService;

public class TflApiClient
{
    private readonly RestClient _client;
    
    public TflApiClient()
    {
        
        RestClientOptions options = new RestClientOptions("https://api.tfl.gov.uk/");
        _client = new RestClient(
            options,
            configureSerialization: s => s.UseSystemTextJson());
    }
    
    public T GetApiResponse<T>(string resource, Dictionary<string, string> queryOptions = null )
    {
        var request = new RestRequest(resource);

        if(queryOptions != null)
        {
            foreach (var queryKey in queryOptions.Keys)
            {
                request.AddParameter(queryKey, queryOptions[queryKey]);
            }
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