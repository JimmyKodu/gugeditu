# Google Maps Device Tracking System / 谷歌地图设备追踪系统

An ASP.NET Core MVC application that integrates Google Maps to display device locations across China and the USA.

## Features / 功能特性

- 🗺️ **Google Maps Integration** - Full integration with Google Maps API
- 🇨🇳 **China Mainland Mode** - Special configuration for users in mainland China to avoid connection timeouts
- 🌍 **Multi-Region Support** - Display devices in China provinces and USA states
- 🔄 **Configuration Switch** - Toggle between `maps.googleapis.com` and `maps.google.cn`
- 📍 **Device Tracking** - Track and display multiple device locations with detailed information

## Configuration / 配置

### China Mainland Mode / 中国大陆模式

The application includes a special configuration switch for users in mainland China to avoid the common `ERR_CONNECTION_TIMED_OUT` error when accessing `maps.googleapis.com`.

**How it works:**

1. Toggle the "中国大陆模式" (China Mainland Mode) switch in the UI
2. When enabled, the application loads Google Maps from `https://maps.google.cn/maps/api/js` instead of `https://maps.googleapis.com/maps/api/js`
3. The switch also adjusts coordinate systems (GCJ02 for China mainland users)

**Configuration file:**

Edit `src/Gugeditu.Web/appsettings.json`:

```json
{
  "MapsConfig": {
    "IsChinaMainland": true
  }
}
```

- Set to `true` for China mainland users
- Set to `false` for international users

## Prerequisites / 前置要求

- .NET 8.0 SDK
- Modern web browser with JavaScript enabled
- Internet connection (for Google Maps API)

## Getting Started / 快速开始

### 1. Clone the repository / 克隆仓库

```bash
git clone https://github.com/JimmyKodu/gugeditu.git
cd gugeditu
```

### 2. Build the project / 构建项目

```bash
cd src/Gugeditu.Web
dotnet restore
dotnet build
```

### 3. Run the application / 运行应用

```bash
dotnet run
```

The application will start at `http://localhost:5000` (or `https://localhost:5001` for HTTPS).

### 4. Open in browser / 在浏览器中打开

Navigate to `http://localhost:5000` in your web browser.

## Usage / 使用方法

### Viewing China Devices / 查看中国设备

Click the "🇨🇳 显示中国设备" button to display devices across Chinese provinces.

The map will show devices in major provincial capitals including:
- Beijing, Shanghai, Guangzhou, Shenzhen
- Chengdu, Hangzhou, Wuhan, Xi'an
- And 24 more provincial capitals

### Viewing USA Devices / 查看美国设备

Click the "🇺🇸 显示美国设备" button to display devices across USA states.

The map will show devices in major US cities including:
- Los Angeles, New York, Houston, Miami
- Chicago, Philadelphia, Phoenix, Atlanta
- And 7 more major cities

### Switching Map Provider / 切换地图提供商

Use the toggle switch labeled "中国大陆模式" to switch between:
- **OFF**: Uses `maps.googleapis.com` (standard international API)
- **ON**: Uses `maps.google.cn` (China mirror to avoid connection timeout)

## Project Structure / 项目结构

```
gugeditu/
├── src/
│   └── Gugeditu.Web/
│       ├── Controllers/           # MVC Controllers
│       │   ├── HomeController.cs
│       │   └── MapsConfigController.cs
│       ├── Models/                # Data models
│       │   ├── ICoordinate.cs
│       │   ├── Coordinate.cs
│       │   ├── MapsConfig.cs
│       │   └── DeviceRepository.cs
│       ├── Views/                 # Razor views
│       │   └── Home/
│       │       └── Index.cshtml
│       ├── appsettings.json       # Configuration
│       └── Program.cs             # Application entry point
├── pic/                           # Screenshots
│   ├── china-provinces.png
│   └── usa-states.png
└── tools/                         # Utility scripts
    └── generate_screenshots.py
```

## API Endpoints / API 端点

### Get China Devices / 获取中国设备

```
GET /Home/GetChinaDevices
```

Returns JSON array of devices located in Chinese provinces.

### Get USA Devices / 获取美国设备

```
GET /Home/GetUSADevices
```

Returns JSON array of devices located in USA states.

### Get Map Configuration / 获取地图配置

```
GET /MapsConfig/GetConfig
```

Returns current map configuration including API URL.

## Technical Details / 技术细节

### Coordinate Systems / 坐标系统

The application supports multiple coordinate systems:

- **WGS84**: Standard GPS coordinates (Lat/Lon)
- **GCJ02**: Chinese coordinate system (used when China Mainland Mode is enabled)
- **BD09**: Baidu coordinate system (stored for compatibility)

### Data Model / 数据模型

The `ICoordinate` interface includes:
- Device identification (Sn, Id)
- Location data (Lat, Lon, GCJLat, GCJLon, etc.)
- Device metrics (Speed, Direction, Height)
- Signal information (Satellites, SignalStrength, Power)
- Timestamps (ReportingTime, CreateTime)

## Screenshots / 屏幕截图

Screenshots are available in the `pic/` directory:

- `china-provinces.png` - Map showing devices across Chinese provinces
- `usa-states.png` - Map showing devices across USA states

## Troubleshooting / 故障排除

### Google Maps not loading / 谷歌地图无法加载

**Issue**: Map shows blank or connection timeout

**Solution**:
1. Check your internet connection
2. If in mainland China, enable "中国大陆模式" switch
3. Refresh the page after toggling the switch

### Devices not showing on map / 设备未在地图上显示

**Issue**: Map loads but no device markers appear

**Solution**:
1. Click the corresponding button (China or USA)
2. Wait a few seconds for markers to load
3. Check browser console for any JavaScript errors

## Contributing / 贡献

Contributions are welcome! Please feel free to submit a Pull Request.

## License / 许可证

This project is open source and available under the MIT License.

## Support / 支持

For issues, questions, or suggestions, please open an issue on GitHub.
