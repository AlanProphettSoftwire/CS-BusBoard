using System.Net;
using BusBoard.ConsoleApp;
using BusBoard.ConsoleApp.TflModels;
public class Program
{
	private static void LoadMenu()
	{
		Console.Clear();
		Console.WriteLine("🚌 Welcome To Bus Board");
	}
	public static void Main(string[] args)
	{
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		LoadMenu();
		TflApiService tflApiClient = new TflApiService();
		var predictions = tflApiClient.GetApiResponse<List<Prediction>>("StopPoint/490005911W/Arrivals");
	}
}