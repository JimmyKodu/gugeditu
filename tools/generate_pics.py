"""生成 pic/ 目录下的两张设备地图截图（示意图）。

脚本在不依赖外部图层服务的情况下，以本项目 `DeviceRepository` 中相同的坐标，
在等距圆柱投影的画布上画出：
- 中国大陆：34 个省级行政区设备点（使用 GCJ02 偏移坐标，对齐 maps.google.cn 底图）。
- 美国：50 个州设备点（使用 WGS84 原始坐标）。

图片同时还原了前端工具栏（区域单选 + 跨境连接开关）的视觉效果，用来体现
应用在默认（开关关闭，走 maps.google.cn 国内镜像）与打开（走服务器跨境代理
回源 maps.googleapis.com）两种模式下的运行效果。
"""
from __future__ import annotations

import os
import math
from PIL import Image, ImageDraw, ImageFont


CHINA_PROVINCES = [
    ("Beijing",       39.9042, 116.4074),
    ("Tianjin",       39.3434, 117.3616),
    ("Hebei",         38.0428, 114.5149),
    ("Shanxi",        37.8706, 112.5489),
    ("Inner Mongolia",40.8175, 111.7656),
    ("Liaoning",      41.8057, 123.4315),
    ("Jilin",         43.8868, 125.3245),
    ("Heilongjiang",  45.8038, 126.5349),
    ("Shanghai",      31.2304, 121.4737),
    ("Jiangsu",       32.0603, 118.7969),
    ("Zhejiang",      30.2741, 120.1551),
    ("Anhui",         31.8257, 117.2272),
    ("Fujian",        26.0745, 119.2965),
    ("Jiangxi",       28.6765, 115.8921),
    ("Shandong",      36.6512, 117.1201),
    ("Henan",         34.7657, 113.7535),
    ("Hubei",         30.5928, 114.3055),
    ("Hunan",         28.2282, 112.9388),
    ("Guangdong",     23.1291, 113.2644),
    ("Guangxi",       22.8240, 108.3669),
    ("Hainan",        20.0174, 110.3492),
    ("Chongqing",     29.5630, 106.5516),
    ("Sichuan",       30.5728, 104.0668),
    ("Guizhou",       26.5982, 106.7074),
    ("Yunnan",        25.0389, 102.7183),
    ("Xizang",        29.6520,  91.1721),
    ("Shaanxi",       34.3416, 108.9398),
    ("Gansu",         36.0611, 103.8343),
    ("Qinghai",       36.6171, 101.7782),
    ("Ningxia",       38.4872, 106.2309),
    ("Xinjiang",      43.7930,  87.6278),
    ("Taiwan",        25.0330, 121.5654),
    ("Hong Kong",     22.3193, 114.1694),
    ("Macau",         22.1987, 113.5439),
]

US_STATES = [
    ("Alabama",       32.3770, -86.3006),
    ("Alaska",        58.3019, -134.4197),
    ("Arizona",       33.4484, -112.0740),
    ("Arkansas",      34.7465, -92.2896),
    ("California",    38.5767, -121.4934),
    ("Colorado",      39.7392, -104.9903),
    ("Connecticut",   41.7658, -72.6734),
    ("Delaware",      39.1582, -75.5244),
    ("Florida",       30.4383, -84.2807),
    ("Georgia",       33.7490, -84.3880),
    ("Hawaii",        21.3099, -157.8581),
    ("Idaho",         43.6150, -116.2023),
    ("Illinois",      39.7980, -89.6544),
    ("Indiana",       39.7684, -86.1581),
    ("Iowa",          41.5868, -93.6250),
    ("Kansas",        39.0483, -95.6780),
    ("Kentucky",      38.1867, -84.8753),
    ("Louisiana",     30.4571, -91.1874),
    ("Maine",         44.3106, -69.7795),
    ("Maryland",      38.9784, -76.4922),
    ("Massachusetts", 42.3601, -71.0589),
    ("Michigan",      42.7335, -84.5555),
    ("Minnesota",     44.9537, -93.0900),
    ("Mississippi",   32.2988, -90.1848),
    ("Missouri",      38.5767, -92.1735),
    ("Montana",       46.5891, -112.0391),
    ("Nebraska",      40.8136, -96.7026),
    ("Nevada",        39.1638, -119.7674),
    ("New Hampshire", 43.2081, -71.5376),
    ("New Jersey",    40.2206, -74.7597),
    ("New Mexico",    35.6870, -105.9378),
    ("New York",      42.6526, -73.7562),
    ("N Carolina",    35.7796, -78.6382),
    ("N Dakota",      46.8209, -100.7837),
    ("Ohio",          39.9612, -82.9988),
    ("Oklahoma",      35.4676, -97.5164),
    ("Oregon",        44.9429, -123.0351),
    ("Pennsylvania",  40.2732, -76.8867),
    ("Rhode Island",  41.8240, -71.4128),
    ("S Carolina",    34.0007, -81.0348),
    ("S Dakota",      44.3683, -100.3510),
    ("Tennessee",     36.1627, -86.7816),
    ("Texas",         30.2672, -97.7431),
    ("Utah",          40.7608, -111.8910),
    ("Vermont",       44.2601, -72.5754),
    ("Virginia",      37.5407, -77.4360),
    ("Washington",    47.0379, -122.9007),
    ("W Virginia",    38.3498, -81.6326),
    ("Wisconsin",     43.0747, -89.3841),
    ("Wyoming",       41.1400, -104.8202),
]


CHINA_OUTLINE = [
    (73.5, 39.5), (75.0, 37.5), (74.9, 36.9), (76.0, 35.5), (77.5, 35.4),
    (79.0, 34.3), (78.8, 33.4), (79.0, 32.5), (80.3, 30.4), (81.3, 30.1),
    (82.2, 30.0), (83.5, 28.3), (85.0, 27.9), (86.2, 27.9), (88.1, 27.5),
    (89.5, 28.0), (91.7, 27.7), (94.2, 29.0), (96.6, 28.4), (97.5, 27.6),
    (98.3, 26.0), (97.8, 24.4), (98.9, 24.1), (99.5, 22.9), (101.1, 22.4),
    (102.1, 22.4), (103.3, 22.8), (104.8, 22.8), (106.7, 22.0), (108.0, 21.5),
    (109.5, 21.4), (110.4, 21.2), (110.5, 20.1), (112.0, 21.6), (114.0, 22.5),
    (116.5, 22.9), (118.2, 24.5), (120.0, 26.8), (120.5, 27.8), (121.0, 29.0),
    (121.3, 31.8), (121.7, 33.0), (120.7, 34.5), (120.2, 36.9), (121.7, 37.3),
    (122.4, 37.5), (122.6, 38.7), (121.2, 39.1), (121.3, 40.0), (122.2, 40.8),
    (124.0, 40.1), (125.4, 42.5), (128.2, 41.8), (130.6, 42.5), (131.3, 44.1),
    (133.0, 45.2), (134.8, 47.7), (133.5, 48.4), (130.8, 48.4), (130.2, 50.0),
    (127.0, 50.0), (125.5, 53.3), (121.1, 53.5), (120.8, 52.6), (117.9, 49.5),
    (116.7, 49.8), (115.6, 47.9), (111.4, 45.1), (107.5, 42.5), (103.4, 41.8),
    (100.8, 42.7), (97.2, 42.8), (95.0, 44.3), (91.5, 45.2), (90.7, 45.5),
    (87.3, 49.1), (84.7, 47.0), (82.5, 45.1), (79.9, 44.9), (80.4, 42.9),
    (80.2, 41.8), (76.8, 41.0), (74.9, 40.4), (73.8, 39.8),
    (73.5, 39.5),
]

US_OUTLINE = [
    (-124.7, 48.4), (-123.3, 46.1), (-123.9, 42.0), (-124.2, 40.0),
    (-123.0, 38.0), (-121.6, 36.5), (-120.6, 34.5), (-118.5, 34.0),
    (-117.2, 32.5), (-114.8, 32.7), (-111.1, 31.3), (-108.2, 31.8),
    (-106.5, 31.8), (-103.0, 28.9), (-101.0, 29.8), (-99.3, 26.4),
    (-97.4, 25.8), (-97.2, 27.8), (-95.6, 28.7), (-94.0, 29.8),
    (-92.0, 29.2), (-89.5, 29.0), (-88.0, 30.4), (-85.6, 30.1),
    (-83.9, 30.0), (-82.3, 26.9), (-80.5, 25.2), (-80.2, 25.8),
    (-80.1, 27.2), (-80.6, 28.6), (-81.2, 31.5), (-79.9, 32.8),
    (-78.0, 33.9), (-76.4, 35.2), (-75.6, 37.2), (-75.0, 38.7),
    (-74.3, 39.5), (-74.1, 40.7), (-72.9, 41.2), (-71.5, 41.5),
    (-70.7, 41.7), (-70.0, 41.9), (-70.6, 42.9), (-70.8, 43.7),
    (-69.0, 44.0), (-67.8, 44.9), (-67.0, 44.7), (-68.8, 46.9),
    (-69.2, 47.5), (-70.9, 45.3), (-74.7, 45.0), (-76.0, 43.9),
    (-79.0, 43.3), (-82.4, 41.7), (-83.2, 42.0), (-82.5, 45.0),
    (-84.6, 46.0), (-88.0, 47.4), (-89.8, 48.0), (-94.7, 48.9),
    (-97.2, 49.0), (-104.0, 49.0), (-110.0, 49.0), (-115.5, 49.0),
    (-122.8, 49.0), (-124.7, 48.4),
]


def mkfont(size: int):
    for path in (
        "/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf",
        "/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf",
    ):
        if os.path.exists(path):
            return ImageFont.truetype(path, size)
    return ImageFont.load_default()


def render(title_lines, outline, devices, bbox, outfile, cross_border_on):
    W, H = 1280, 800
    toolbar_h = 64
    map_w, map_h = W - 40, H - toolbar_h - 40

    img = Image.new("RGB", (W, H), (246, 248, 250))
    d = ImageDraw.Draw(img)

    d.rectangle((0, 0, W, toolbar_h), fill=(255, 255, 255))
    d.line((0, toolbar_h, W, toolbar_h), fill=(208, 215, 222))
    f_md = mkfont(16)
    f_sm = mkfont(13)
    f_lg = mkfont(20)
    d.text((20, 10), title_lines[0], fill=(36, 41, 47), font=f_lg)
    d.text((20, 40), title_lines[1], fill=(87, 96, 106), font=f_sm)

    sw_x, sw_y = W - 420, 22
    d.text((sw_x - 90, sw_y + 3), "Cross-border", fill=(36, 41, 47), font=f_md)
    track = (sw_x, sw_y, sw_x + 44, sw_y + 22)
    d.rounded_rectangle(track, radius=11, fill=(45, 164, 78) if cross_border_on else (203, 213, 225))
    knob_x = sw_x + 22 if cross_border_on else sw_x + 2
    d.ellipse((knob_x, sw_y + 2, knob_x + 18, sw_y + 20), fill=(255, 255, 255))
    state = "ON  -  proxy /proxy/maps/api/js -> maps.googleapis.com" if cross_border_on \
            else "OFF  -  maps.google.cn (mainland China mirror)"
    d.text((sw_x + 54, sw_y + 3), state, fill=(87, 96, 106), font=f_sm)

    mx0, my0 = 20, toolbar_h + 20
    mx1, my1 = mx0 + map_w, my0 + map_h
    d.rectangle((mx0, my0, mx1, my1), fill=(226, 239, 252), outline=(158, 183, 218))

    min_lon, min_lat, max_lon, max_lat = bbox

    def project(lon, lat):
        x = mx0 + (lon - min_lon) / (max_lon - min_lon) * map_w
        y = my1 - (lat - min_lat) / (max_lat - min_lat) * map_h
        return x, y

    lo = math.floor(min_lon / 10) * 10
    while lo <= max_lon:
        x, _ = project(lo, min_lat)
        d.line((x, my0, x, my1), fill=(200, 220, 240))
        lo += 10
    la = math.floor(min_lat / 10) * 10
    while la <= max_lat:
        _, y = project(min_lon, la)
        d.line((mx0, y, mx1, y), fill=(200, 220, 240))
        la += 10

    poly = [project(lon, lat) for lon, lat in outline]
    d.polygon(poly, fill=(232, 245, 233), outline=(46, 125, 50))

    for name, lat, lon in devices:
        x, y = project(lon, lat)
        d.ellipse((x - 6, y - 14, x + 6, y - 2), fill=(207, 34, 46), outline=(255, 255, 255))
        d.polygon([(x - 4, y - 4), (x + 4, y - 4), (x, y + 3)], fill=(207, 34, 46))
        d.ellipse((x - 2, y - 11, x + 2, y - 7), fill=(255, 255, 255))
        label = name
        try:
            bb = d.textbbox((0, 0), label, font=f_sm)
            tw, th = bb[2] - bb[0], bb[3] - bb[1]
        except Exception:
            tw, th = 60, 12
        tx = x + 7
        ty = y - 9
        if tx + tw + 4 > mx1:
            tx = x - tw - 9
        d.rectangle((tx - 2, ty - 1, tx + tw + 2, ty + th + 1), fill=(255, 255, 255))
        d.text((tx, ty), label, fill=(36, 41, 47), font=f_sm)

    stat = "Devices: %d" % len(devices)
    d.rectangle((mx1 - 150, my1 - 36, mx1 - 10, my1 - 10), fill=(255, 255, 255), outline=(208, 215, 222))
    d.text((mx1 - 140, my1 - 30), stat, fill=(36, 41, 47), font=f_md)

    img.save(outfile, "PNG", optimize=True)


def main():
    here = os.path.dirname(os.path.abspath(__file__))
    out_dir = os.path.abspath(os.path.join(here, "..", "pic"))
    os.makedirs(out_dir, exist_ok=True)

    render(
        title_lines=(
            "China Mainland devices - one per province (34 total)",
            "Source: /api/devices/china   Script: https://maps.google.cn/maps/api/js?callback=initMap",
        ),
        outline=CHINA_OUTLINE,
        devices=CHINA_PROVINCES,
        bbox=(72.0, 16.0, 136.0, 55.0),
        outfile=os.path.join(out_dir, "china-provinces.png"),
        cross_border_on=False,
    )

    render(
        title_lines=(
            "United States devices - all 50 states",
            "Source: /api/devices/us   Script: /proxy/maps/api/js?callback=initMap (proxied to maps.googleapis.com)",
        ),
        outline=US_OUTLINE,
        devices=US_STATES,
        bbox=(-168.0, 18.0, -66.0, 50.0),
        outfile=os.path.join(out_dir, "us-states.png"),
        cross_border_on=True,
    )

    print("Wrote:", os.listdir(out_dir))


if __name__ == "__main__":
    main()
