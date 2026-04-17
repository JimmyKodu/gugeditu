namespace Gugeditu.Web.Models;

/// <summary>
/// 地图配置类
/// </summary>
public class MapsConfig
{
    /// <summary>
    /// 是否为中国大陆用户
    /// </summary>
    public bool IsChinaMainland { get; set; }

    /// <summary>
    /// 获取Google Maps API URL
    /// </summary>
    public string GetMapsApiUrl()
    {
        // 中国大陆用户使用google.cn镜像，避免连接超时
        // 非大陆用户使用标准googleapis.com
        return IsChinaMainland
            ? "https://maps.google.cn/maps/api/js"
            : "https://maps.googleapis.com/maps/api/js";
    }
}
