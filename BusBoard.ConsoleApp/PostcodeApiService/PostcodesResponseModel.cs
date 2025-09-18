namespace BusBoard.ConsoleApp.PostcodeApiService;

using System.Text.Json.Serialization;

public class PostcodeResponseModel
{
    [JsonPropertyName("result")]
    public PostcodeResult Result { get; set; }
}

public class PostcodeResult
{
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
}