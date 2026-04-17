namespace Gugeditu.Web.Models;

/// <summary>
/// 设备坐标上报接口。定义一个设备在某一时刻上报的地理位置信息及相关
/// 传感器 / 通讯数据。为了同时兼容国内的多套坐标系（WGS84 / GCJ-02 / BD-09），
/// 接口保留了多组经纬度字段。
/// </summary>
public partial interface ICoordinate
{
    #region 属性

    /// <summary>自增主键 Id。</summary>
    Int32 Id { get; set; }

    /// <summary>设备序列号。</summary>
    String? Sn { get; set; }

    /// <summary>设备上报时间（服务端接收到报文的时间）。</summary>
    DateTime ReportingTime { get; set; }

    /// <summary>设备上报时间戳（Unix 毫秒），便于前端直接排序 / 回放轨迹。</summary>
    Int64 ReportingTime1 { get; set; }

    /// <summary>纬度（WGS84 / GPS 原始坐标）。</summary>
    Double Lat { get; set; }

    /// <summary>经度（WGS84 / GPS 原始坐标）。</summary>
    Double Lon { get; set; }

    /// <summary>速度（km/h）。</summary>
    Double Speed { get; set; }

    /// <summary>航向角（度，0 正北顺时针）。</summary>
    Double Direction { get; set; }

    /// <summary>海拔高度（米）。</summary>
    Double Height { get; set; }

    /// <summary>定位精度半径（米）。</summary>
    Double Radius { get; set; }

    /// <summary>原始数据报文（Hex / JSON 等）。</summary>
    String? Data { get; set; }

    /// <summary>无效标记：0 有效，其它为无效原因码。</summary>
    Int32 Invalid { get; set; }

    /// <summary>入库创建时间。</summary>
    DateTime CreateTime { get; set; }

    /// <summary>GCJ（火星坐标系）纬度。</summary>
    Double GCJLat { get; set; }

    /// <summary>GCJ（火星坐标系）经度。</summary>
    Double GCJLon { get; set; }

    /// <summary>BD09（百度坐标系）纬度。</summary>
    Double BD09Lat { get; set; }

    /// <summary>BD09（百度坐标系）经度。</summary>
    Double BD09Lon { get; set; }

    /// <summary>GCJ02（高德 / 谷歌中国版坐标系）纬度。</summary>
    Double GCJ02Lat { get; set; }

    /// <summary>GCJ02（高德 / 谷歌中国版坐标系）经度。</summary>
    Double GCJ02Lon { get; set; }

    /// <summary>卫星颗数。</summary>
    Int32 Satellites { get; set; }

    /// <summary>信号强度（dBm 或百分比）。</summary>
    Double SignalStrength { get; set; }

    /// <summary>电量百分比（0-100）。</summary>
    Int32 Power { get; set; }

    #endregion
}
