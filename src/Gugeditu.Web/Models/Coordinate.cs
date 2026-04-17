namespace Gugeditu.Web.Models;

/// <summary>
/// 坐标实体类
/// </summary>
public class Coordinate : ICoordinate
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
}
