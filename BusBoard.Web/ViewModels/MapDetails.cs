namespace BusBoard.Web.ViewModels;

public class MapDetails
{
    private static readonly double LondonLongitude = 0.1276;
    private static readonly  double LondonLatitude = 51.5072;
    
    public string PostCode { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    
    public string Message { get; set; }
    
    public List<StopPoint> NearbyStopPoints { get; set; }
    
    public MapDetails(string? postCode, double? latitude, double? longitude,  List<StopPoint>? nearbyStopPoints, string? message)
    {
        Message = message ?? "";
        PostCode = postCode ?? "";
        Longitude = longitude ?? LondonLongitude;
        Latitude = latitude ?? LondonLatitude;
        NearbyStopPoints = nearbyStopPoints ?? [];
    }

}