using frontendmusicshark.Middlewares;
using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregamos los servicios
builder.Services.AddControllersWithViews();

// Soporte para consultar el API
var UrlWebAPI = builder.Configuration["UrlWebAPI"];
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<SendBearerDelegatingHandler>();
builder.Services.AddTransient<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<AuthClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); });
builder.Services.AddHttpClient<MusicGenreClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<UserClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<RoleClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<SongClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<ProfileClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<FileClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();
builder.Services.AddHttpClient<LogBookClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<SendBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefreshTokenDelegatingHandler>();

// Soporte para Cookie Auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = ".musicShark";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.LoginPath = "/Auth";
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Agregamos un middleware para el manejo de errores
app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();