using Microsoft.AspNetCore.Mvc;
using Gugeditu.Web.Models;

namespace Gugeditu.Web.Controllers;

/// <summary>
/// 地图配置控制器
/// </summary>
public class MapsConfigController : Controller
{
    private readonly IConfiguration _configuration;

    public MapsConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// 获取地图配置
    /// </summary>
    [HttpGet]
    public IActionResult GetConfig()
    {
        var config = new MapsConfig
        {
            IsChinaMainland = _configuration.GetValue<bool>("MapsConfig:IsChinaMainland")
        };

        return Json(new
        {
            isChinaMainland = config.IsChinaMainland,
            apiUrl = config.GetMapsApiUrl()
        });
    }

    /// <summary>
    /// 更新地图配置（切换中国大陆模式）
    /// </summary>
    [HttpPost]
    public IActionResult UpdateConfig([FromBody] MapsConfig config)
    {
        // 在实际应用中，这里应该更新配置文件或数据库
        // 这里仅作为演示返回配置信息
        return Json(new
        {
            success = true,
            message = "配置已更新",
            isChinaMainland = config.IsChinaMainland,
            apiUrl = config.GetMapsApiUrl()
        });
    }
}
