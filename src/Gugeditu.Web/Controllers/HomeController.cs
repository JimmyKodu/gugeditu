using System.Diagnostics;
using Gugeditu.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gugeditu.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var devices = new[]
        {
            new CoordinateDeviceViewModel
            {
                Sn = "CN-GD-0001",
                Country = "中国",
                Region = "广东省",
                Lat = ConvertDdmmToDecimal(2310.5354),
                Lon = ConvertDdmmToDecimal(11319.6602),
                ReportingTime = DateTime.Parse("2026-04-16T08:20:00Z"),
                Speed = 32.5,
                Direction = 120.6,
                Power = 87
            },
            new CoordinateDeviceViewModel
            {
                Sn = "US-CA-0001",
                Country = "美国",
                Region = "California",
                Lat = ConvertDdmmToDecimal(3746.4940),
                Lon = ConvertDdmmToDecimal(12225.1640),
                ReportingTime = DateTime.Parse("2026-04-16T08:22:00Z"),
                Speed = 45.2,
                Direction = 82.3,
                Power = 74
            }
        };

        return View(devices);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static double ConvertDdmmToDecimal(double raw)
    {
        var degree = Math.Truncate(raw / 100);
        var minute = raw - (degree * 100);
        return degree + (minute / 60d);
    }
}
