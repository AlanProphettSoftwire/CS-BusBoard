namespace BusBoard.Api.Models;

public class StopPoint
{
    public string Id { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
    public double Distance { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}

public class Prediction
{
    public string Id { get; set; } = string.Empty;
    public string LineName { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public int TimeToStation { get; set; }
    public DateTime ExpectedArrival { get; set; }
    public string StationName { get; set; } = string.Empty;
}

public class BusStop
{
    public string StopName { get; set; } = string.Empty;
    public string StopId { get; set; } = string.Empty;
    public double Distance { get; set; }
    public List<BusDeparture> Departures { get; set; } = new();
}

public class BusDeparture
{
    public string Route { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public int MinutesToArrival { get; set; }
    public DateTime ExpectedArrival { get; set; }
}
