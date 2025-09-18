using System.Text.Json.Serialization;

public class StopPointsResponse
{
    [JsonPropertyName("stopPoints")]
    public List<StopPoint> StopPoints { get; set; }
    
}

public class StopPoint
{
    [JsonPropertyName("naptanId")]
    public string NaptanId { get; set; }
    
    [JsonPropertyName("indicator")]
    public string Indicator { get; set; }

    [JsonPropertyName("commonName")]
    public string CommonName { get; set; }

    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}

