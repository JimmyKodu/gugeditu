using Microsoft.AspNetCore.Mvc;

namespace Gugeditu.Web.Controllers;

/// <summary>
/// 谷歌地图 JS 跨境代理。
///
/// 背景：在中国大陆，直连
///     https://maps.googleapis.com/maps/api/js
/// 经常出现 <c>ERR_CONNECTION_TIMED_OUT</c>。本控制器提供两条路径：
/// <list type="bullet">
///   <item>
///     <description>
///       <b>默认（开关关闭）：</b>前端直接请求
///       <c>https://maps.google.cn/maps/api/js</c>，这是谷歌面向中国大陆
///       专门保留的镜像域名，国内网络可达。
///     </description>
///   </item>
///   <item>
///     <description>
///       <b>开关打开 —— 跨境连接：</b>前端改为请求本控制器
///       <c>/proxy/maps/api/js</c>，由服务器（部署在境外 / 可直连 Google 的
///       出口节点）作为反向代理回源 <c>maps.googleapis.com</c>，把返回的
///       JavaScript 回传给浏览器。这样浏览器完全不接触
///       <c>maps.googleapis.com</c>，避免了 <c>ERR_CONNECTION_TIMED_OUT</c>。
///     </description>
///   </item>
/// </list>
/// </summary>
[ApiController]
[Route("proxy/maps")]
public class MapsProxyController : ControllerBase
{
    // Google Maps JS 加载器允许的查询参数白名单，避免把用户输入原样拼到上游 URL。
    private static readonly HashSet<string> AllowedParameters = new(StringComparer.OrdinalIgnoreCase)
    {
        "key", "callback", "libraries", "v", "language", "region", "map_ids", "channel", "client",
    };

    private static readonly Uri UpstreamBase = new("https://maps.googleapis.com/");

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<MapsProxyController> _logger;

    public MapsProxyController(IHttpClientFactory httpClientFactory, ILogger<MapsProxyController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <summary>
    /// 代理 <c>/maps/api/js</c> 请求。前端通过
    /// <c>&lt;script src="/proxy/maps/api/js?callback=initMap&amp;key=..."&gt;</c>
    /// 加载地图脚本。
    /// </summary>
    [HttpGet("api/js")]
    public async Task<IActionResult> MapsJs(CancellationToken cancellationToken)
    {
        // 只透传白名单中的查询参数，防止 SSRF / 开放代理滥用。
        var query = new List<string>();
        foreach (var kv in Request.Query)
        {
            if (!AllowedParameters.Contains(kv.Key)) continue;
            foreach (var v in kv.Value)
            {
                if (v is null) continue;
                query.Add($"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(v)}");
            }
        }

        // 严格按固定路径回源，路径部分由服务端完全控制，不受请求参数影响，
        // 避免被构造为任意上游 URL。
        var upstream = new Uri(UpstreamBase, "/maps/api/js" + (query.Count > 0 ? "?" + string.Join('&', query) : string.Empty));

        var client = _httpClientFactory.CreateClient("GoogleMaps");
        try
        {
            using var upstreamResponse = await client.GetAsync(upstream, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var content = await upstreamResponse.Content.ReadAsByteArrayAsync(cancellationToken);

            var contentType = upstreamResponse.Content.Headers.ContentType?.ToString()
                              ?? "application/javascript; charset=utf-8";

            // 透传状态码，便于浏览器按正常 script 错误处理逻辑工作。
            Response.StatusCode = (int)upstreamResponse.StatusCode;
            return File(content, contentType);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to proxy Google Maps JS from {Upstream}", upstream);
            // 用一段 JS 友好地把错误抛到前端控制台，方便用户看到提示。
            var fallback = "console.error('[MapsProxy] 跨境连接失败: " + ex.GetType().Name + "');";
            return new ContentResult
            {
                StatusCode  = StatusCodes.Status502BadGateway,
                ContentType = "application/javascript; charset=utf-8",
                Content     = fallback,
            };
        }
    }
}
