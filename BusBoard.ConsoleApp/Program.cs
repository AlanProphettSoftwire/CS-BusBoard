using System.Net;
using BusBoard.ConsoleApp.PostcodeApiService;
using BusBoard.ConsoleApp.TflApiService;
using BusBoard.ConsoleApp.TflModels;
using Microsoft.Extensions.Configuration;

public class Program
{
	private static void LoadMenu()
	{
		Console.WriteLine("## Welcome To Bus Board ##");
	}

	public static string RequestUserForStopID()
	{
		string userInput = "";
		do
		{
			Console.WriteLine("Enter the bus stop id:\n");
			userInput = Console.ReadLine();
		} while (string.IsNullOrWhiteSpace(userInput));

		return userInput;
	}

	public static string NormaliseUserPostcode(string? postcode)
	{
		if (string.IsNullOrWhiteSpace(postcode))
		{
			return "";
		}
		return postcode.Replace(" ", "").Trim();
	}
	
	public static string RequestUserForPostcode()
	{
		string userInput = "";
		do
		{
			Console.WriteLine("Enter your postcode:\n");
			userInput = Console.ReadLine();
			userInput = NormaliseUserPostcode(userInput);
		} while (string.IsNullOrWhiteSpace(userInput));

		return userInput;
	}

	public static void DisplayBusPredictions(List<Prediction> predictions)
	{
		foreach (var prediction in predictions)
		{
			Console.WriteLine($"\tRoute: {prediction.LineName} | Destination: {prediction.DestinationName} | ETA: {prediction.TimeToStation}s");
		}
	}

	public static void LoadUiForViewingNextFiveUpcomingBusesAtBusStop()
	{
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

		var config = new ConfigurationBuilder()
			.AddUserSecrets<Program>()
			.AddEnvironmentVariables()
			.Build();
		
		Actions tflActions = new Actions(config);
		LoadMenu();
		Actions tflActions = new Actions();
		var userInputStopId = RequestUserForStopID();
		var prediction = tflActions.GetUpToNextFiveBusPredictionsAtStop(userInputStopId);
		DisplayBusPredictions(prediction);
	}

	public static void LoadUiForViewingNearbyBusStops()
	{
		LoadMenu();
		Actions tflActions = new Actions();
		PostcodeApiClient postcodeApiClientInstance = new PostcodeApiClient();
		var userInputPostcode = RequestUserForPostcode();
		PostcodeResponseModel postcodeApiResponse = postcodeApiClientInstance.GetLonLatFromPostcode(userInputPostcode);
		List<StopPoint> nearbyStops = tflActions.GetStopsInRadiusOfLocation(postcodeApiResponse.Result.Latitude, postcodeApiResponse.Result.Longitude);
		Console.WriteLine($"{nearbyStops.Count} stops nearby.");
		foreach (var stop in nearbyStops)
		{
			Console.WriteLine($"{stop.CommonName} - {stop.Indicator} ({(int)stop.Distance} meters away)");
			var prediction = tflActions.GetUpToNextFiveBusPredictionsAtStop(stop.NaptanId);
			DisplayBusPredictions(prediction);
		}
   	}
	public static void Main(string[] args)
	{
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		LoadUiForViewingNearbyBusStops();
	}
}