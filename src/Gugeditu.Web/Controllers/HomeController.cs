using Microsoft.AspNetCore.Mvc;
using Gugeditu.Web.Models;

namespace Gugeditu.Web.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        var mapsConfig = new MapsConfig
        {
            IsChinaMainland = _configuration.GetValue<bool>("MapsConfig:IsChinaMainland")
        };

        return View(mapsConfig);
    }

    /// <summary>
    /// 获取中国省份设备数据
    /// </summary>
    [HttpGet]
    public IActionResult GetChinaDevices()
    {
        var devices = DeviceRepository.GetChinaProvinceDevices();
        return Json(devices);
    }

    /// <summary>
    /// 获取美国州设备数据
    /// </summary>
    [HttpGet]
    public IActionResult GetUSADevices()
    {
        var devices = DeviceRepository.GetUSAStateDevices();
        return Json(devices);
    }
}
