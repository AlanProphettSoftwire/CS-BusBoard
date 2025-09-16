namespace BusBoard.ConsoleApp.TflApiService;
using BusBoard.ConsoleApp.TflModels;


public class Actions
{
    private readonly TflApiClient _tflApiClient;

    public List<Prediction> GetUpToNextFiveBusPredictionsAtStop(string stopId)
    {
        var predictions = _tflApiClient.GetApiResponse<List<Prediction>>($"StopPoint/{stopId}/Arrivals");
        predictions = predictions.OrderBy(x => x.TimeToStation).Take(5).ToList();
        return predictions;
    }
}