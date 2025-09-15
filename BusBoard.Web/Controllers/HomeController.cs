using Microsoft.AspNetCore.Mvc;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;
using BusBoard.Api.Services;

namespace BusBoard.Web.Controllers;

public class HomeController : Controller
{
    private readonly ITflApiService _tflApiService;

    public HomeController(ITflApiService tflApiService)
    {
        _tflApiService = tflApiService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> BusInfo(PostcodeSelection selection)
    {
        if (string.IsNullOrWhiteSpace(selection.Postcode))
        {
            var emptyInfo = new BusInfo(string.Empty);
            return View(emptyInfo);
        }

        try
        {
            var busStops = await _tflApiService.GetBusStopsNearPostcodeAsync(selection.Postcode);
            var info = new BusInfo(selection.Postcode)
            {
                BusStops = busStops
            };
            return View(info);
        }
        catch (Exception ex)
        {
            // Log error in production
            ViewBag.Error = "Unable to fetch bus information at this time. Please try again later.";
            var errorInfo = new BusInfo(selection.Postcode);
            return View(errorInfo);
        }
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