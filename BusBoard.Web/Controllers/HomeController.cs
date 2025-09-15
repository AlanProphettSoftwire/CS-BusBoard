using Microsoft.AspNetCore.Mvc;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult BusInfo(PostcodeSelection selection)
    {
        // Add some properties to the BusInfo view model with the data you want to render on the page.
        // Write code here to populate the view model with info from the APIs.
        // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses.
        var info = new BusInfo(selection.Postcode ?? string.Empty);
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