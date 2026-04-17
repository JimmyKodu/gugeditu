# gugeditu — ASP.NET Core MVC 接入谷歌地图

基于 `ICoordinate` 接口的设备坐标模型，在页面上用谷歌地图分别绘制：

- **中国大陆设备**：34 个省级行政区（含直辖市 / 自治区 / 特别行政区），精确到省份一台。
- **美国设备**：全部 50 个州，一州一台。

同时提供一个 **"跨境连接" 开关**，用来解决国内直连 `maps.googleapis.com`
常见的 `ERR_CONNECTION_TIMED_OUT` 问题。

## 目录结构

```
.
├── src/Gugeditu.Web/             # ASP.NET Core 8 MVC 站点
│   ├── Models/
│   │   ├── ICoordinate.cs        # 题目给定接口
│   │   ├── Coordinate.cs         # ICoordinate 的实现
│   │   └── DeviceRepository.cs   # 34 省 + 50 州演示数据 & 坐标转换
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── DevicesApiController.cs    # /api/devices/china | /api/devices/us
│   │   ├── MapsConfigController.cs    # /api/config/maps
│   │   └── MapsProxyController.cs     # /proxy/maps/api/js  (跨境代理)
│   ├── Views/Home/Index.cshtml   # 地图页 + 工具栏 + 开关
│   └── appsettings.json          # GoogleMaps:ApiKey 等配置
├── pic/                          # 地图截图（与 src 同级）
│   ├── china-provinces.png
│   └── us-states.png
└── tools/generate_pics.py        # 截图生成脚本
```

## 运行

```bash
cd src/Gugeditu.Web
dotnet build
dotnet run
```

在 `appsettings.json` 中填入自己的 `GoogleMaps:ApiKey`。浏览器打开
`http://localhost:5xxx/` 即可看到地图；工具栏里切换 *中国大陆 / 美国* 切换区域。

## 跨境连接开关的实现原理

| 开关状态 | 前端实际加载的脚本 | 适用网络 |
|---|---|---|
| **OFF（默认）** | `https://maps.google.cn/maps/api/js?callback=initMap` | 中国大陆：`maps.google.cn` 是谷歌面向大陆保留的镜像域名，可直连，不会出现 `ERR_CONNECTION_TIMED_OUT` |
| **ON（跨境）**   | `/proxy/maps/api/js?callback=initMap`（由 `MapsProxyController` 反向代理回源 `maps.googleapis.com`） | 服务器部署在境外 / 可直连 Google 的节点时，浏览器完全不直接访问 `maps.googleapis.com`，绕过国内网络对 `googleapis.com` 的拦截 |

关键点：

1. **默认即适配中国大陆用户**：未打开跨境开关时走 `maps.google.cn`，避免 `ERR_CONNECTION_TIMED_OUT`。
2. **中国大陆视图使用 GCJ02 坐标**：与 `maps.google.cn` 的火星坐标底图对齐，不会有偏移。
   `DeviceRepository` 里对每条设备都同时生成了 WGS84 / GCJ02 / BD09 三套坐标。
3. **跨境模式下**：前端拉取原始 WGS84 坐标 + 走代理访问
   `maps.googleapis.com`，代理仅放行白名单查询参数，目标 URL 完全由服务端固定，
   防止被当成开放代理滥用。
4. **可在 `appsettings.json` 里关闭 `GoogleMaps:CrossBorderEnabled`** 强制锁定为国内镜像。

## 接口

- `GET /api/devices/china` &rarr; 34 条 `Coordinate` JSON
- `GET /api/devices/us` &rarr; 50 条 `Coordinate` JSON
- `GET /api/config/maps` &rarr; `{ apiKey, cnScriptBase, proxyScriptBase, crossBorderEnabled }`
- `GET /proxy/maps/api/js?callback=initMap&key=...` &rarr; 透传 Google Maps 加载器 JS

## 截图

- `pic/china-provinces.png` —— 开关 **关闭**（走 `maps.google.cn`）下的中国大陆设备分布。
- `pic/us-states.png` —— 开关 **打开**（走 `/proxy/maps/api/js`）下的美国设备分布。

截图由 `tools/generate_pics.py` 基于与运行时一致的坐标数据渲染生成：

```bash
pip install Pillow
python3 tools/generate_pics.py
```
