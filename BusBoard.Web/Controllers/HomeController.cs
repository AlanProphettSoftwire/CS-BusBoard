using System.Runtime.InteropServices.JavaScript;
using BusBoard.Api.PostcodeApiService;
using BusBoard.Api.TflModels;
using Microsoft.AspNetCore.Mvc;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;
using BusBoard.Api.TflApiService;

namespace BusBoard.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(PostcodeSelection selection)
    {
        PostcodeApiClient postcodeApiClient = new PostcodeApiClient();

        if (string.IsNullOrWhiteSpace(selection.Postcode))
        {
            var mapDetails = new MapDetails(selection.Postcode, null, null, null, null);
            return View(mapDetails);
        }

        try
        {
            PostcodeResponseModel postcodeLongLat = postcodeApiClient.GetLonLatFromPostcode(selection.Postcode);
            Actions tflActions = new Actions();
            try
            {
                List<StopPoint> stopPoints = tflActions.GetStopsInRadiusOfLocation(postcodeLongLat.Result.Latitude,
                    postcodeLongLat.Result.Longitude, radius: 600);

                if (stopPoints.Count > 0)
                {
                    var mapDetails = new MapDetails(selection.Postcode, postcodeLongLat.Result.Latitude,
                        postcodeLongLat.Result.Longitude, stopPoints, null);
                    return View(mapDetails);
                }
                else
                {
                    var mapDetails = new MapDetails(selection.Postcode, postcodeLongLat.Result.Latitude,
                        postcodeLongLat.Result.Longitude, stopPoints, "☹️ No stops nearby");
                    return View(mapDetails);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        catch (Exception ex)
        {
            var mapDetails = new MapDetails(selection.Postcode, null, null, null, $"🗺️ Your postcode '{selection.Postcode}' does not exist.");
            return View(mapDetails);
        }
    }

    [HttpGet]
    public IActionResult StopInfo(string stopId)
    {
        // Add some properties to the BusInfo view model with the data you want to render on the page.
        // Write code here to populate the view model with info from the APIs.
        // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses.
        
        Actions tflActions = new Actions();

        StopPoint? stopPoint = tflActions.GetStopPoint(stopId);
        
        List<Prediction> prediction = tflActions.GetUpToNextFiveBusPredictionsAtStop(stopId);
        
        var info = new StopInfo(stopPoint, prediction);
        return View(info);
    }

    public IActionResult About()
    {
        ViewBag.Message = "Information about this site";
        return View();
    }

    public IActionResult Contact()
    {
        ViewBag.Message = "Contact us!";
        return View();
    }
}