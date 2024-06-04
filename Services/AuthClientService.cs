using frontendmusicshark.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace frontendmusicshark.Services;

public class AuthClientService(HttpClient client, IHttpContextAccessor httpContextAccessor)
{
    public async Task<AuthUser> GetTokenAsync(string email, string password)
    {
        Login usuario = new() { Email = email, Password = password };
        // Realizo la llamada al Web API
        var response = await client.PostAsJsonAsync("api", usuario);
        var token = await response.Content.ReadFromJsonAsync<AuthUser>();
        
        return token!;
    }

    public async void LogInAsync(List<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();
        await httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties)!;
    }
}