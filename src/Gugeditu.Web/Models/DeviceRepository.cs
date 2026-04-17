namespace Gugeditu.Web.Models;

/// <summary>
/// 设备数据仓库。演示用内存数据源：每个省份（中国大陆）/ 每个州（美国）放置一个
/// 虚拟设备，坐标取该行政区划的大致几何中心。
/// </summary>
public static class DeviceRepository
{
    /// <summary>
    /// 中国大陆（含直辖市 / 自治区 / 特别行政区），共 34 个省级行政区。
    /// 坐标为各省大致几何中心的 WGS84 经纬度。
    /// </summary>
    private static readonly (string Region, double Lat, double Lon)[] ChinaProvinces = new[]
    {
        ("北京市",       39.9042,  116.4074),
        ("天津市",       39.3434,  117.3616),
        ("河北省",       38.0428,  114.5149),
        ("山西省",       37.8706,  112.5489),
        ("内蒙古自治区", 40.8175,  111.7656),
        ("辽宁省",       41.8057,  123.4315),
        ("吉林省",       43.8868,  125.3245),
        ("黑龙江省",     45.8038,  126.5349),
        ("上海市",       31.2304,  121.4737),
        ("江苏省",       32.0603,  118.7969),
        ("浙江省",       30.2741,  120.1551),
        ("安徽省",       31.8257,  117.2272),
        ("福建省",       26.0745,  119.2965),
        ("江西省",       28.6765,  115.8921),
        ("山东省",       36.6512,  117.1201),
        ("河南省",       34.7657,  113.7535),
        ("湖北省",       30.5928,  114.3055),
        ("湖南省",       28.2282,  112.9388),
        ("广东省",       23.1291,  113.2644),
        ("广西壮族自治区",22.8240, 108.3669),
        ("海南省",       20.0174,  110.3492),
        ("重庆市",       29.5630,  106.5516),
        ("四川省",       30.5728,  104.0668),
        ("贵州省",       26.5982,  106.7074),
        ("云南省",       25.0389,  102.7183),
        ("西藏自治区",   29.6520,  91.1721 ),
        ("陕西省",       34.3416,  108.9398),
        ("甘肃省",       36.0611,  103.8343),
        ("青海省",       36.6171,  101.7782),
        ("宁夏回族自治区",38.4872, 106.2309),
        ("新疆维吾尔自治区",43.7930,87.6278),
        ("台湾省",       25.0330,  121.5654),
        ("香港特别行政区",22.3193, 114.1694),
        ("澳门特别行政区",22.1987, 113.5439),
    };

    /// <summary>美国 50 个州，坐标使用各州州府的经纬度（WGS84）。</summary>
    private static readonly (string Region, double Lat, double Lon)[] UsStates = new[]
    {
        ("Alabama",        32.3770, -86.3006),
        ("Alaska",         58.3019, -134.4197),
        ("Arizona",        33.4484, -112.0740),
        ("Arkansas",       34.7465, -92.2896),
        ("California",     38.5767, -121.4934),
        ("Colorado",       39.7392, -104.9903),
        ("Connecticut",    41.7658, -72.6734),
        ("Delaware",       39.1582, -75.5244),
        ("Florida",        30.4383, -84.2807),
        ("Georgia",        33.7490, -84.3880),
        ("Hawaii",         21.3099, -157.8581),
        ("Idaho",          43.6150, -116.2023),
        ("Illinois",       39.7980, -89.6544),
        ("Indiana",        39.7684, -86.1581),
        ("Iowa",           41.5868, -93.6250),
        ("Kansas",         39.0483, -95.6780),
        ("Kentucky",       38.1867, -84.8753),
        ("Louisiana",      30.4571, -91.1874),
        ("Maine",          44.3106, -69.7795),
        ("Maryland",       38.9784, -76.4922),
        ("Massachusetts",  42.3601, -71.0589),
        ("Michigan",       42.7335, -84.5555),
        ("Minnesota",      44.9537, -93.0900),
        ("Mississippi",    32.2988, -90.1848),
        ("Missouri",       38.5767, -92.1735),
        ("Montana",        46.5891, -112.0391),
        ("Nebraska",       40.8136, -96.7026),
        ("Nevada",         39.1638, -119.7674),
        ("New Hampshire",  43.2081, -71.5376),
        ("New Jersey",     40.2206, -74.7597),
        ("New Mexico",     35.6870, -105.9378),
        ("New York",       42.6526, -73.7562),
        ("North Carolina", 35.7796, -78.6382),
        ("North Dakota",   46.8209, -100.7837),
        ("Ohio",           39.9612, -82.9988),
        ("Oklahoma",       35.4676, -97.5164),
        ("Oregon",         44.9429, -123.0351),
        ("Pennsylvania",   40.2732, -76.8867),
        ("Rhode Island",   41.8240, -71.4128),
        ("South Carolina", 34.0007, -81.0348),
        ("South Dakota",   44.3683, -100.3510),
        ("Tennessee",      36.1627, -86.7816),
        ("Texas",          30.2672, -97.7431),
        ("Utah",           40.7608, -111.8910),
        ("Vermont",        44.2601, -72.5754),
        ("Virginia",       37.5407, -77.4360),
        ("Washington",     47.0379, -122.9007),
        ("West Virginia",  38.3498, -81.6326),
        ("Wisconsin",      43.0747, -89.3841),
        ("Wyoming",        41.1400, -104.8202),
    };

    /// <summary>生成中国大陆设备列表（按省份一台）。</summary>
    public static IReadOnlyList<Coordinate> GetChinaDevices() => Build("CN", ChinaProvinces);

    /// <summary>生成美国设备列表（按州一台）。</summary>
    public static IReadOnlyList<Coordinate> GetUsDevices() => Build("US", UsStates);

    private static IReadOnlyList<Coordinate> Build(
        string country,
        IReadOnlyList<(string Region, double Lat, double Lon)> source)
    {
        // 使用固定种子，保证每次请求返回相同的演示数据。
        var random = new Random(country.GetHashCode());
        var now = DateTime.UtcNow;
        var list = new List<Coordinate>(source.Count);
        for (int i = 0; i < source.Count; i++)
        {
            var (region, lat, lon) = source[i];
            var (gcjLat, gcjLon) = WgsToGcj02(lat, lon);
            var (bdLat, bdLon) = Gcj02ToBd09(gcjLat, gcjLon);
            var reported = now.AddMinutes(-random.Next(0, 60));

            list.Add(new Coordinate
            {
                Id              = i + 1,
                Sn              = $"{country}-DEV-{(i + 1):D3}",
                ReportingTime   = reported,
                ReportingTime1  = new DateTimeOffset(reported).ToUnixTimeMilliseconds(),
                Lat             = lat,
                Lon             = lon,
                Speed           = Math.Round(random.NextDouble() * 5, 2),
                Direction       = Math.Round(random.NextDouble() * 360, 1),
                Height          = Math.Round(random.NextDouble() * 1500, 1),
                Radius          = Math.Round(5 + random.NextDouble() * 20, 1),
                Data            = null,
                Invalid         = 0,
                CreateTime      = reported,
                GCJLat          = gcjLat,
                GCJLon          = gcjLon,
                BD09Lat         = bdLat,
                BD09Lon         = bdLon,
                GCJ02Lat        = gcjLat,
                GCJ02Lon        = gcjLon,
                Satellites      = random.Next(6, 16),
                SignalStrength  = -Math.Round(50 + random.NextDouble() * 40, 1),
                Power           = random.Next(20, 100),
                Region          = region,
                Country         = country,
            });
        }
        return list;
    }

    #region 坐标转换（WGS84 -> GCJ02 -> BD09）

    private const double PI = 3.1415926535897932384626;
    private const double A  = 6378245.0;
    private const double EE = 0.00669342162296594323;

    /// <summary>
    /// 判断是否在中国境外（粗略）。境外直接返回原坐标，不做偏移。
    /// </summary>
    private static bool OutOfChina(double lat, double lon)
        => lon < 72.004 || lon > 137.8347 || lat < 0.8293 || lat > 55.8271;

    private static double TransformLat(double x, double y)
    {
        double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
        ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;
        ret += (20.0 * Math.Sin(y * PI) + 40.0 * Math.Sin(y / 3.0 * PI)) * 2.0 / 3.0;
        ret += (160.0 * Math.Sin(y / 12.0 * PI) + 320.0 * Math.Sin(y * PI / 30.0)) * 2.0 / 3.0;
        return ret;
    }

    private static double TransformLon(double x, double y)
    {
        double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
        ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;
        ret += (20.0 * Math.Sin(x * PI) + 40.0 * Math.Sin(x / 3.0 * PI)) * 2.0 / 3.0;
        ret += (150.0 * Math.Sin(x / 12.0 * PI) + 300.0 * Math.Sin(x / 30.0 * PI)) * 2.0 / 3.0;
        return ret;
    }

    /// <summary>WGS84 -> GCJ02 偏移算法。</summary>
    private static (double Lat, double Lon) WgsToGcj02(double lat, double lon)
    {
        if (OutOfChina(lat, lon)) return (lat, lon);
        double dLat = TransformLat(lon - 105.0, lat - 35.0);
        double dLon = TransformLon(lon - 105.0, lat - 35.0);
        double radLat = lat / 180.0 * PI;
        double magic = Math.Sin(radLat);
        magic = 1 - EE * magic * magic;
        double sqrtMagic = Math.Sqrt(magic);
        dLat = (dLat * 180.0) / ((A * (1 - EE)) / (magic * sqrtMagic) * PI);
        dLon = (dLon * 180.0) / (A / sqrtMagic * Math.Cos(radLat) * PI);
        return (lat + dLat, lon + dLon);
    }

    /// <summary>GCJ02 -> BD09 偏移算法。</summary>
    private static (double Lat, double Lon) Gcj02ToBd09(double gcjLat, double gcjLon)
    {
        double z = Math.Sqrt(gcjLon * gcjLon + gcjLat * gcjLat) + 0.00002 * Math.Sin(gcjLat * PI * 3000.0 / 180.0);
        double theta = Math.Atan2(gcjLat, gcjLon) + 0.000003 * Math.Cos(gcjLon * PI * 3000.0 / 180.0);
        return (z * Math.Sin(theta) + 0.006, z * Math.Cos(theta) + 0.0065);
    }

    #endregion
}
