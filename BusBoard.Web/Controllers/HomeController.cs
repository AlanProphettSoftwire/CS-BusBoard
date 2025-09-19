using System.Runtime.InteropServices.JavaScript;
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
        return View();
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