namespace BusBoard.ConsoleApp.TflApiService;
using BusBoard.ConsoleApp.TflModels;
using Microsoft.Extensions.Configuration;


public class Actions
{
    private readonly TflApiClient _tflApiClient;

    public Actions(IConfiguration secretOptions)
    {
        _tflApiClient = new TflApiClient(secretOptions);
    }

    public List<Prediction> GetUpToNextFiveBusPredictionsAtStop(string stopId)
    {
        var predictions = _tflApiClient.GetApiResponse<List<Prediction>>($"StopPoint/{stopId}/Arrivals");
        predictions = predictions.OrderBy(x => x.TimeToStation).Take(5).ToList();
        return predictions;
    }
}