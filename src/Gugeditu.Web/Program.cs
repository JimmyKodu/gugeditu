var builder = WebApplication.CreateBuilder(args);

// 常规 MVC + JSON API（本项目同时提供视图和 JSON 接口）。
builder.Services.AddControllersWithViews();

// 为 Google Maps JS 跨境代理配置带合理超时的 HttpClient。
builder.Services.AddHttpClient("GoogleMaps", client =>
{
    client.Timeout = TimeSpan.FromSeconds(15);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("GugedituMapsProxy/1.0");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
