namespace Gugeditu.Web.Models;

/// <summary>
/// 设备数据仓库
/// </summary>
public class DeviceRepository
{
    /// <summary>
    /// 获取中国省份设备数据（每个省会城市一个设备）
    /// </summary>
    public static List<Coordinate> GetChinaProvinceDevices()
    {
        var devices = new List<Coordinate>
        {
            // 北京
            new Coordinate { Id = 1, Sn = "CN-BJ-001", Lat = 39.9042, Lon = 116.4074, GCJ02Lat = 39.9042, GCJ02Lon = 116.4074, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 上海
            new Coordinate { Id = 2, Sn = "CN-SH-001", Lat = 31.2304, Lon = 121.4737, GCJ02Lat = 31.2304, GCJ02Lon = 121.4737, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 广州
            new Coordinate { Id = 3, Sn = "CN-GD-001", Lat = 23.1291, Lon = 113.2644, GCJ02Lat = 23.1291, GCJ02Lon = 113.2644, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 深圳
            new Coordinate { Id = 4, Sn = "CN-GD-002", Lat = 22.5431, Lon = 114.0579, GCJ02Lat = 22.5431, GCJ02Lon = 114.0579, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 成都
            new Coordinate { Id = 5, Sn = "CN-SC-001", Lat = 30.5728, Lon = 104.0668, GCJ02Lat = 30.5728, GCJ02Lon = 104.0668, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 杭州
            new Coordinate { Id = 6, Sn = "CN-ZJ-001", Lat = 30.2741, Lon = 120.1551, GCJ02Lat = 30.2741, GCJ02Lon = 120.1551, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 武汉
            new Coordinate { Id = 7, Sn = "CN-HB-001", Lat = 30.5928, Lon = 114.3055, GCJ02Lat = 30.5928, GCJ02Lon = 114.3055, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 西安
            new Coordinate { Id = 8, Sn = "CN-SX-001", Lat = 34.3416, Lon = 108.9398, GCJ02Lat = 34.3416, GCJ02Lon = 108.9398, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 重庆
            new Coordinate { Id = 9, Sn = "CN-CQ-001", Lat = 29.5630, Lon = 106.5516, GCJ02Lat = 29.5630, GCJ02Lon = 106.5516, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 天津
            new Coordinate { Id = 10, Sn = "CN-TJ-001", Lat = 39.3434, Lon = 117.3616, GCJ02Lat = 39.3434, GCJ02Lon = 117.3616, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 南京
            new Coordinate { Id = 11, Sn = "CN-JS-001", Lat = 32.0603, Lon = 118.7969, GCJ02Lat = 32.0603, GCJ02Lon = 118.7969, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 沈阳
            new Coordinate { Id = 12, Sn = "CN-LN-001", Lat = 41.8057, Lon = 123.4315, GCJ02Lat = 41.8057, GCJ02Lon = 123.4315, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 长春
            new Coordinate { Id = 13, Sn = "CN-JL-001", Lat = 43.8171, Lon = 125.3235, GCJ02Lat = 43.8171, GCJ02Lon = 125.3235, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 哈尔滨
            new Coordinate { Id = 14, Sn = "CN-HL-001", Lat = 45.8038, Lon = 126.5340, GCJ02Lat = 45.8038, GCJ02Lon = 126.5340, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 济南
            new Coordinate { Id = 15, Sn = "CN-SD-001", Lat = 36.6512, Lon = 117.1209, GCJ02Lat = 36.6512, GCJ02Lon = 117.1209, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 郑州
            new Coordinate { Id = 16, Sn = "CN-HN-001", Lat = 34.7466, Lon = 113.6253, GCJ02Lat = 34.7466, GCJ02Lon = 113.6253, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 太原
            new Coordinate { Id = 17, Sn = "CN-SX2-001", Lat = 37.8706, Lon = 112.5489, GCJ02Lat = 37.8706, GCJ02Lon = 112.5489, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 石家庄
            new Coordinate { Id = 18, Sn = "CN-HE-001", Lat = 38.0428, Lon = 114.5149, GCJ02Lat = 38.0428, GCJ02Lon = 114.5149, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 呼和浩特
            new Coordinate { Id = 19, Sn = "CN-NM-001", Lat = 40.8414, Lon = 111.7519, GCJ02Lat = 40.8414, GCJ02Lon = 111.7519, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 乌鲁木齐
            new Coordinate { Id = 20, Sn = "CN-XJ-001", Lat = 43.8256, Lon = 87.6168, GCJ02Lat = 43.8256, GCJ02Lon = 87.6168, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 拉萨
            new Coordinate { Id = 21, Sn = "CN-XZ-001", Lat = 29.6520, Lon = 91.1721, GCJ02Lat = 29.6520, GCJ02Lon = 91.1721, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 西宁
            new Coordinate { Id = 22, Sn = "CN-QH-001", Lat = 36.6171, Lon = 101.7782, GCJ02Lat = 36.6171, GCJ02Lon = 101.7782, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 兰州
            new Coordinate { Id = 23, Sn = "CN-GS-001", Lat = 36.0611, Lon = 103.8343, GCJ02Lat = 36.0611, GCJ02Lon = 103.8343, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 银川
            new Coordinate { Id = 24, Sn = "CN-NX-001", Lat = 38.4872, Lon = 106.2309, GCJ02Lat = 38.4872, GCJ02Lon = 106.2309, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 昆明
            new Coordinate { Id = 25, Sn = "CN-YN-001", Lat = 25.0406, Lon = 102.7129, GCJ02Lat = 25.0406, GCJ02Lon = 102.7129, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 贵阳
            new Coordinate { Id = 26, Sn = "CN-GZ-001", Lat = 26.6470, Lon = 106.6302, GCJ02Lat = 26.6470, GCJ02Lon = 106.6302, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 南宁
            new Coordinate { Id = 27, Sn = "CN-GX-001", Lat = 22.8170, Lon = 108.3665, GCJ02Lat = 22.8170, GCJ02Lon = 108.3665, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 海口
            new Coordinate { Id = 28, Sn = "CN-HI-001", Lat = 20.0444, Lon = 110.1999, GCJ02Lat = 20.0444, GCJ02Lon = 110.1999, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 长沙
            new Coordinate { Id = 29, Sn = "CN-HN2-001", Lat = 28.2282, Lon = 112.9388, GCJ02Lat = 28.2282, GCJ02Lon = 112.9388, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 南昌
            new Coordinate { Id = 30, Sn = "CN-JX-001", Lat = 28.6829, Lon = 115.8579, GCJ02Lat = 28.6829, GCJ02Lon = 115.8579, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 福州
            new Coordinate { Id = 31, Sn = "CN-FJ-001", Lat = 26.0745, Lon = 119.2965, GCJ02Lat = 26.0745, GCJ02Lon = 119.2965, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // 合肥
            new Coordinate { Id = 32, Sn = "CN-AH-001", Lat = 31.8206, Lon = 117.2272, GCJ02Lat = 31.8206, GCJ02Lon = 117.2272, CreateTime = DateTime.Now, ReportingTime = DateTime.Now }
        };

        return devices;
    }

    /// <summary>
    /// 获取美国州设备数据（主要州的首府或大城市）
    /// </summary>
    public static List<Coordinate> GetUSAStateDevices()
    {
        var devices = new List<Coordinate>
        {
            // California - Los Angeles
            new Coordinate { Id = 101, Sn = "US-CA-001", Lat = 34.0522, Lon = -118.2437, GCJ02Lat = 34.0522, GCJ02Lon = -118.2437, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // New York
            new Coordinate { Id = 102, Sn = "US-NY-001", Lat = 40.7128, Lon = -74.0060, GCJ02Lat = 40.7128, GCJ02Lon = -74.0060, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Texas - Houston
            new Coordinate { Id = 103, Sn = "US-TX-001", Lat = 29.7604, Lon = -95.3698, GCJ02Lat = 29.7604, GCJ02Lon = -95.3698, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Florida - Miami
            new Coordinate { Id = 104, Sn = "US-FL-001", Lat = 25.7617, Lon = -80.1918, GCJ02Lat = 25.7617, GCJ02Lon = -80.1918, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Illinois - Chicago
            new Coordinate { Id = 105, Sn = "US-IL-001", Lat = 41.8781, Lon = -87.6298, GCJ02Lat = 41.8781, GCJ02Lon = -87.6298, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Pennsylvania - Philadelphia
            new Coordinate { Id = 106, Sn = "US-PA-001", Lat = 39.9526, Lon = -75.1652, GCJ02Lat = 39.9526, GCJ02Lon = -75.1652, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Arizona - Phoenix
            new Coordinate { Id = 107, Sn = "US-AZ-001", Lat = 33.4484, Lon = -112.0740, GCJ02Lat = 33.4484, GCJ02Lon = -112.0740, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Georgia - Atlanta
            new Coordinate { Id = 108, Sn = "US-GA-001", Lat = 33.7490, Lon = -84.3880, GCJ02Lat = 33.7490, GCJ02Lon = -84.3880, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Massachusetts - Boston
            new Coordinate { Id = 109, Sn = "US-MA-001", Lat = 42.3601, Lon = -71.0589, GCJ02Lat = 42.3601, GCJ02Lon = -71.0589, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Washington - Seattle
            new Coordinate { Id = 110, Sn = "US-WA-001", Lat = 47.6062, Lon = -122.3321, GCJ02Lat = 47.6062, GCJ02Lon = -122.3321, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Colorado - Denver
            new Coordinate { Id = 111, Sn = "US-CO-001", Lat = 39.7392, Lon = -104.9903, GCJ02Lat = 39.7392, GCJ02Lon = -104.9903, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Oregon - Portland
            new Coordinate { Id = 112, Sn = "US-OR-001", Lat = 45.5152, Lon = -122.6784, GCJ02Lat = 45.5152, GCJ02Lon = -122.6784, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Nevada - Las Vegas
            new Coordinate { Id = 113, Sn = "US-NV-001", Lat = 36.1699, Lon = -115.1398, GCJ02Lat = 36.1699, GCJ02Lon = -115.1398, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Michigan - Detroit
            new Coordinate { Id = 114, Sn = "US-MI-001", Lat = 42.3314, Lon = -83.0458, GCJ02Lat = 42.3314, GCJ02Lon = -83.0458, CreateTime = DateTime.Now, ReportingTime = DateTime.Now },
            // Minnesota - Minneapolis
            new Coordinate { Id = 115, Sn = "US-MN-001", Lat = 44.9778, Lon = -93.2650, GCJ02Lat = 44.9778, GCJ02Lon = -93.2650, CreateTime = DateTime.Now, ReportingTime = DateTime.Now }
        };

        return devices;
    }
}
