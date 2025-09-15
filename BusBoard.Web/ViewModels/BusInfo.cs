using BusBoard.Api.Models;

namespace BusBoard.Web.ViewModels;

public class BusInfo
{
    public BusInfo(string postCode)
    {
        PostCode = postCode;
    }

    public string PostCode { get; set; } = string.Empty;
    public List<BusStop> BusStops { get; set; } = new();
}