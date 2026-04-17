namespace Gugeditu.Web.Models;

/// <summary>
/// 坐标接口
/// </summary>
public partial interface ICoordinate
{
    #region 属性
    /// <summary>
    /// 主键
    /// </summary>
    Int32 Id { get; set; }

    /// <summary>
    /// 设备序列号
    /// </summary>
    String? Sn { get; set; }

    /// <summary>
    /// 上报时间
    /// </summary>
    DateTime ReportingTime { get; set; }

    /// <summary>
    /// 上报时间(时间戳)
    /// </summary>
    Int64 ReportingTime1 { get; set; }

    /// <summary>
    /// 纬度
    /// </summary>
    Double Lat { get; set; }

    /// <summary>
    /// 经度
    /// </summary>
    Double Lon { get; set; }

    /// <summary>
    /// 速度
    /// </summary>
    Double Speed { get; set; }

    /// <summary>
    /// 方向
    /// </summary>
    Double Direction { get; set; }

    /// <summary>
    /// 高度
    /// </summary>
    Double Height { get; set; }

    /// <summary>
    /// 半径
    /// </summary>
    Double Radius { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    String? Data { get; set; }

    /// <summary>
    /// 无效标志
    /// </summary>
    Int32 Invalid { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreateTime { get; set; }

    /// <summary>
    /// GCJ纬度
    /// </summary>
    Double GCJLat { get; set; }

    /// <summary>
    /// GCJ经度
    /// </summary>
    Double GCJLon { get; set; }

    /// <summary>
    /// BD09纬度
    /// </summary>
    Double BD09Lat { get; set; }

    /// <summary>
    /// BD09经度
    /// </summary>
    Double BD09Lon { get; set; }

    /// <summary>
    /// GCJ02纬度
    /// </summary>
    Double GCJ02Lat { get; set; }

    /// <summary>
    /// GCJ02经度
    /// </summary>
    Double GCJ02Lon { get; set; }

    /// <summary>
    /// 卫星数
    /// </summary>
    Int32 Satellites { get; set; }

    /// <summary>
    /// 信号强度
    /// </summary>
    Double SignalStrength { get; set; }

    /// <summary>
    /// 电量
    /// </summary>
    Int32 Power { get; set; }
    #endregion
}
