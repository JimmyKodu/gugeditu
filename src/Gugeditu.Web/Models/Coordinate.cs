namespace Gugeditu.Web.Models;

/// <summary>
/// <see cref="ICoordinate"/> 的标准实现。用于向前端序列化为 JSON，
/// 因此使用公共可读写自动属性。
/// </summary>
public partial class Coordinate : ICoordinate
{
    public Int32 Id { get; set; }
    public String? Sn { get; set; }
    public DateTime ReportingTime { get; set; }
    public Int64 ReportingTime1 { get; set; }
    public Double Lat { get; set; }
    public Double Lon { get; set; }
    public Double Speed { get; set; }
    public Double Direction { get; set; }
    public Double Height { get; set; }
    public Double Radius { get; set; }
    public String? Data { get; set; }
    public Int32 Invalid { get; set; }
    public DateTime CreateTime { get; set; }
    public Double GCJLat { get; set; }
    public Double GCJLon { get; set; }
    public Double BD09Lat { get; set; }
    public Double BD09Lon { get; set; }
    public Double GCJ02Lat { get; set; }
    public Double GCJ02Lon { get; set; }
    public Int32 Satellites { get; set; }
    public Double SignalStrength { get; set; }
    public Int32 Power { get; set; }

    /// <summary>
    /// 省份 / 州名称。非 <see cref="ICoordinate"/> 接口字段，仅用于
    /// 前端地图 InfoWindow 展示。
    /// </summary>
    public String? Region { get; set; }

    /// <summary>国家代码（CN / US 等），用于按地区分组。</summary>
    public String? Country { get; set; }
}
