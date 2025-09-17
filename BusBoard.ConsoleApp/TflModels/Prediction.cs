namespace BusBoard.ConsoleApp.TflModels;
using System.Text.Json.Serialization;
public class Prediction
{
    [JsonPropertyName("lineName")]
    public string LineName { get; set; }

    [JsonPropertyName("destinationName")]
    public string DestinationName { get; set; }
    
    [JsonPropertyName("timeToStation")]
    public int TimeToStation { get; set; }
}
