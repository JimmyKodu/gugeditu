using Microsoft.AspNetCore.Mvc;

namespace Gugeditu.Web.Controllers;

/// <summary>
/// 向前端暴露地图相关的运行期配置（API Key、默认区域、是否允许跨境开关等）。
/// API Key 保存在服务端配置中，避免硬编码进视图模板。
/// </summary>
[ApiController]
[Route("api/config")]
public class MapsConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public MapsConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("maps")]
    public IActionResult Maps()
    {
        return Ok(new
        {
            apiKey            = _configuration["GoogleMaps:ApiKey"] ?? string.Empty,
            // 国内镜像；默认使用 maps.google.cn，可在配置里覆盖。
            cnScriptBase      = _configuration["GoogleMaps:CnScriptBase"] ?? "https://maps.google.cn/maps/api/js",
            // 跨境代理端点（打开开关后前端改走这里）。
            proxyScriptBase   = _configuration["GoogleMaps:ProxyScriptBase"] ?? "/proxy/maps/api/js",
            // 允许用户使用跨境开关。可在生产环境强制关闭。
            crossBorderEnabled = _configuration.GetValue("GoogleMaps:CrossBorderEnabled", true),
        });
    }
}
