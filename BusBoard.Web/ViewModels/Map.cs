namespace BusBoard.Web.ViewModels;

public class Map
{
    private static readonly double LondonLongitude = 0.1276;
    private static readonly  double LondonLatitude = 51.5072;
    
    public string PostCode { get; set; } = string.Empty;
    public double Longitude { get; set; } = LondonLongitude;
    public double Latitude { get; set; } = LondonLatitude;
    
    public Map(string postCode, double latitude, double longitude)
    {
        PostCode = postCode;
        Longitude = longitude;
        Latitude = latitude;
    }

}