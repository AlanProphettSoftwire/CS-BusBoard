using System.Net;
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
			Console.WriteLine("Enter the bus stop id: ");
			userInput = Console.ReadLine();
		} while (string.IsNullOrWhiteSpace(userInput));

		return userInput;
	}

	public static void DisplayBusPredictions(List<Prediction> predictions)
	{
		foreach (var prediction in predictions)
		{
			Console.WriteLine($"Route: {prediction.LineName} | Destination: {prediction.DestinationName} | ETA: {prediction.TimeToStation}s");
		}
	}

	public static void Main(string[] args)
	{
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

		var config = new ConfigurationBuilder()
			.AddUserSecrets<Program>()
			.AddEnvironmentVariables()
			.Build();
		
		Actions tflActions = new Actions(config);
		LoadMenu();

		var userInputStopId = RequestUserForStopID();
		var prediction = tflActions.GetUpToNextFiveBusPredictionsAtStop(userInputStopId);
		DisplayBusPredictions(prediction);

	}
}