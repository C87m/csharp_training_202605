using src.Presentations.Extensions;
using src.Presentations.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.SettingDependencyInjection(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "CookieAuthentication"; // デフォルトの認証スキームをクッキー認証に設定
})
.AddCookie("CookieAuthentication", options => // "CookieAuthentication" という名前でクッキー認証を設定
{
    options.LoginPath = "/Account/Login"; // 未認証時にリダイレクトされるログインページのパス
    options.AccessDeniedPath = "/Account/AccessDenied"; // 権限がない場合にリダイレクトされるパス
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // クッキーの有効期限
    options.SlidingExpiration = true; // 有効期限をスライドさせる（アクセスがあるたびに延長）
});

// 認可サービスを追加
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CanDeleteData", policy => policy.RequireRole("Admin"));


var app = builder.Build();

app.UseMiddleware<InternalExceptionLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
