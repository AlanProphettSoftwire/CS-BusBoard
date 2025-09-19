namespace BusBoard.Web.ViewModels;
using BusBoard.Api.TflModels;

public class StopInfo
{
    public StopInfo(StopPoint stopPoint, List<Prediction> predictions)
    {
        StopPointDetails = stopPoint;
        Predictions = predictions;
    }

    public StopPoint? StopPointDetails { get; set; }
    public List<Prediction> Predictions { get; set; } = [];
}