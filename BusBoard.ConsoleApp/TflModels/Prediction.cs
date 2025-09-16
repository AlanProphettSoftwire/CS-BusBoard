namespace BusBoard.ConsoleApp.TflModels;
using System.Text.Json.Serialization;
public class Prediction
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("operationType")]
    public int OperationType { get; set; }

    [JsonPropertyName("vehicleId")]
    public string VehicleId { get; set; }

    [JsonPropertyName("naptanId")]
    public string NaptanId { get; set; }

    [JsonPropertyName("stationName")]
    public string StationName { get; set; }

    [JsonPropertyName("lineId")]
    public string LineId { get; set; }

    [JsonPropertyName("lineName")]
    public string LineName { get; set; }

    [JsonPropertyName("platformName")]
    public string PlatformName { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; }

    [JsonPropertyName("bearing")]
    public string Bearing { get; set; }

    [JsonPropertyName("tripId")]
    public string TripId { get; set; }

    [JsonPropertyName("baseVersion")]
    public string BaseVersion { get; set; }

    [JsonPropertyName("destinationNaptanId")]
    public string DestinationNaptanId { get; set; }

    [JsonPropertyName("destinationName")]
    public string DestinationName { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("timeToStation")]
    public int TimeToStation { get; set; }

    [JsonPropertyName("currentLocation")]
    public string CurrentLocation { get; set; }

    [JsonPropertyName("towards")]
    public string Towards { get; set; }

    [JsonPropertyName("expectedArrival")]
    public DateTime ExpectedArrival { get; set; }

    [JsonPropertyName("timeToLive")]
    public DateTime TimeToLive { get; set; }

    [JsonPropertyName("modeName")]
    public string ModeName { get; set; }

    [JsonPropertyName("timing")]
    public PredictionTiming Timing { get; set; }
}

public class PredictionTiming
{
    [JsonPropertyName("countdownServerAdjustment")]
    public string CountdownServerAdjustment { get; set; }

    [JsonPropertyName("source")]
    public DateTime Source { get; set; }

    [JsonPropertyName("insert")]
    public DateTime Insert { get; set; }

    [JsonPropertyName("read")]
    public DateTime Read { get; set; }

    [JsonPropertyName("sent")]
    public DateTime Sent { get; set; }

    [JsonPropertyName("received")]
    public DateTime Received { get; set; }
}
