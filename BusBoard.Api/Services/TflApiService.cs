using System.Text.Json;
using BusBoard.Api.Models;

namespace BusBoard.Api.Services;

public interface ITflApiService
{
    Task<List<BusStop>> GetBusStopsNearPostcodeAsync(string postcode);
}

public class TflApiService : ITflApiService
{
    private readonly HttpClient _httpClient;
    private const string TflBaseUrl = "https://api.tfl.gov.uk";

    public TflApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BusStop>> GetBusStopsNearPostcodeAsync(string postcode)
    {
        try
        {
            // First, get coordinates from postcode
            var coordinates = await GetCoordinatesFromPostcodeAsync(postcode);
            if (coordinates == null) return new List<BusStop>();

            // Then get nearby bus stops
            var stopPoints = await GetNearbyStopPointsAsync(coordinates.Value.Lat, coordinates.Value.Lon);
            
            // Get predictions for each stop
            var busStops = new List<BusStop>();
            foreach (var stop in stopPoints.Take(5)) // Limit to 5 stops for performance
            {
                var predictions = await GetPredictionsForStopAsync(stop.Id);
                var busStop = new BusStop
                {
                    StopName = stop.CommonName,
                    StopId = stop.Id,
                    Distance = stop.Distance,
                    Departures = predictions.Select(p => new BusDeparture
                    {
                        Route = p.LineName,
                        Destination = p.DestinationName,
                        MinutesToArrival = p.TimeToStation / 60, // Convert seconds to minutes
                        ExpectedArrival = p.ExpectedArrival
                    }).OrderBy(d => d.MinutesToArrival).Take(3).ToList() // Top 3 departures per stop
                };
                busStops.Add(busStop);
            }

            return busStops;
        }
        catch (Exception ex)
        {
            // Log error in real application
            Console.WriteLine($"Error fetching bus data: {ex.Message}");
            return new List<BusStop>();
        }
    }

    private async Task<(double Lat, double Lon)?> GetCoordinatesFromPostcodeAsync(string postcode)
    {
        try
        {
            // Using postcodes.io API for UK postcode lookup
            var cleanPostcode = postcode.Replace(" ", "").ToUpperInvariant();
            var response = await _httpClient.GetAsync($"https://api.postcodes.io/postcodes/{Uri.EscapeDataString(cleanPostcode)}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            
            if (doc.RootElement.TryGetProperty("result", out var result))
            {
                if (result.TryGetProperty("latitude", out var latElement) && 
                    result.TryGetProperty("longitude", out var lonElement))
                {
                    return (latElement.GetDouble(), lonElement.GetDouble());
                }
            }
            
            return null;
        }
        catch
        {
            return null;
        }
    }

    private async Task<List<StopPoint>> GetNearbyStopPointsAsync(double lat, double lon)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{TflBaseUrl}/StopPoint?lat={lat}&lon={lon}&stopTypes=NaptanPublicBusCoachTram&radius=500");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            
            var stopPoints = new List<StopPoint>();
            if (doc.RootElement.TryGetProperty("stopPoints", out var stopPointsElement))
            {
                foreach (var stopElement in stopPointsElement.EnumerateArray())
                {
                    var stopPoint = new StopPoint
                    {
                        Id = stopElement.GetProperty("id").GetString() ?? "",
                        CommonName = stopElement.GetProperty("commonName").GetString() ?? "",
                        Distance = stopElement.GetProperty("distance").GetDouble(),
                        Lat = stopElement.GetProperty("lat").GetDouble(),
                        Lon = stopElement.GetProperty("lon").GetDouble()
                    };
                    stopPoints.Add(stopPoint);
                }
            }
            
            return stopPoints.OrderBy(s => s.Distance).ToList();
        }
        catch
        {
            return new List<StopPoint>();
        }
    }

    private async Task<List<Prediction>> GetPredictionsForStopAsync(string stopId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{TflBaseUrl}/StopPoint/{stopId}/Arrivals");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            
            var predictions = new List<Prediction>();
            foreach (var predictionElement in doc.RootElement.EnumerateArray())
            {
                var prediction = new Prediction
                {
                    Id = predictionElement.GetProperty("id").GetString() ?? "",
                    LineName = predictionElement.GetProperty("lineName").GetString() ?? "",
                    DestinationName = predictionElement.GetProperty("destinationName").GetString() ?? "",
                    TimeToStation = predictionElement.GetProperty("timeToStation").GetInt32(),
                    ExpectedArrival = predictionElement.GetProperty("expectedArrival").GetDateTime(),
                    StationName = predictionElement.GetProperty("stationName").GetString() ?? ""
                };
                predictions.Add(prediction);
            }
            
            return predictions.OrderBy(p => p.TimeToStation).ToList();
        }
        catch
        {
            return new List<Prediction>();
        }
    }
}
