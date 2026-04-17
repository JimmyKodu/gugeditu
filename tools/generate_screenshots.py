#!/usr/bin/env python3
"""
Screenshot generation script for Google Maps device visualization
Requires: playwright
Install: pip install playwright && playwright install chromium
"""

import asyncio
import os
from playwright.async_api import async_playwright

async def capture_screenshots():
    """Capture screenshots of China and USA device maps"""

    # Ensure pic directory exists
    pic_dir = "/home/runner/work/gugeditu/gugeditu/pic"
    os.makedirs(pic_dir, exist_ok=True)

    async with async_playwright() as p:
        # Launch browser
        browser = await p.chromium.launch(headless=True)
        context = await browser.new_context(viewport={'width': 1920, 'height': 1080})
        page = await context.new_page()

        # Navigate to the application
        print("Navigating to application...")
        await page.goto('http://localhost:5000', wait_until='networkidle', timeout=60000)

        # Wait for map to load
        print("Waiting for map to initialize...")
        await page.wait_for_selector('#map', timeout=30000)
        await asyncio.sleep(5)  # Extra wait for Google Maps to fully render

        # Capture China devices
        print("Capturing China provinces map...")
        await page.click('button:has-text("显示中国设备")')
        await asyncio.sleep(8)  # Wait for markers to load and render

        china_screenshot_path = os.path.join(pic_dir, 'china-provinces.png')
        await page.screenshot(path=china_screenshot_path, full_page=False)
        print(f"✓ Saved: {china_screenshot_path}")

        # Capture USA devices
        print("Capturing USA states map...")
        await page.click('button:has-text("显示美国设备")')
        await asyncio.sleep(8)  # Wait for markers to load and render

        usa_screenshot_path = os.path.join(pic_dir, 'usa-states.png')
        await page.screenshot(path=usa_screenshot_path, full_page=False)
        print(f"✓ Saved: {usa_screenshot_path}")

        await browser.close()
        print("\n✓ All screenshots captured successfully!")

if __name__ == '__main__':
    asyncio.run(capture_screenshots())
