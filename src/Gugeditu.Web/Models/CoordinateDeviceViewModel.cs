namespace Gugeditu.Web.Models;

public sealed class CoordinateDeviceViewModel
{
    public string Sn { get; init; } = string.Empty;

    public string Region { get; init; } = string.Empty;

    public string Country { get; init; } = string.Empty;

    public double Lat { get; init; }

    public double Lon { get; init; }

    public DateTime ReportingTime { get; init; }

    public double Speed { get; init; }

    public double Direction { get; init; }

    public int Power { get; init; }
}
