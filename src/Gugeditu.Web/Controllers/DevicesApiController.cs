using Microsoft.AspNetCore.Mvc;
using Gugeditu.Web.Models;

namespace Gugeditu.Web.Controllers;

/// <summary>
/// 设备坐标 JSON API，供前端地图脚本按国家 / 区域拉取设备列表。
/// </summary>
[ApiController]
[Route("api/devices")]
public class DevicesApiController : ControllerBase
{
    /// <summary>返回中国大陆省级设备（每省一台）。</summary>
    [HttpGet("china")]
    public IReadOnlyList<Coordinate> China() => DeviceRepository.GetChinaDevices();

    /// <summary>返回美国州级设备（每州一台）。</summary>
    [HttpGet("us")]
    public IReadOnlyList<Coordinate> Us() => DeviceRepository.GetUsDevices();
}
