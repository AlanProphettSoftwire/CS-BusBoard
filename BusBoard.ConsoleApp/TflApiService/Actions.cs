namespace BusBoard.ConsoleApp.TflApiService;
using BusBoard.ConsoleApp.TflModels;
using Microsoft.Extensions.Configuration;


public class Actions
{
    private readonly TflApiClient _tflApiClient;

    public Actions()
    {
        _tflApiClient = new TflApiClient();
    }

    public List<Prediction> GetUpToNextFiveBusPredictionsAtStop(string stopId)
    {
        var predictions = _tflApiClient.GetApiResponse<List<Prediction>>($"StopPoint/{stopId}/Arrivals");
        predictions = predictions.OrderBy(x => x.TimeToStation).Take(5).ToList();
        return predictions;
    }
    
    public List<StopPoint> GetStopsInRadiusOfLocation(double latitude, double longitude, int radius = 200, bool orderByDistance = true)
    {
        Dictionary<string, string> queryOptions = new Dictionary<string, string>
        {
            {"stopTypes", "NaptanPublicBusCoachTram" },
            {"lat", latitude.ToString()},
            {"lon", longitude.ToString()},
            {"radius", radius.ToString()}
        };

        var predictions = _tflApiClient.GetApiResponse<StopPointsResponse>($"StopPoint", queryOptions);

        if (orderByDistance)
        {
            return predictions.StopPoints.OrderBy(stop => stop.Distance).ToList();
        }
        
        return predictions.StopPoints;
    }
}