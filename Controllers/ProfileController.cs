using System.Security.Claims;
using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendmusicshark;

[Authorize]
public class ProfileController(ProfileClientService profile) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        AuthUser? user = null;
        try
        {
            ViewBag.timeRemaining = await profile.GetTimeAsync();
            user = new AuthUser
            {
                Email = User.FindFirstValue(ClaimTypes.Name) ?? "Sin correo",
                Name = User.FindFirstValue(ClaimTypes.GivenName) ?? "Sin nombre",
                Role = User.FindFirstValue(ClaimTypes.Role) ?? "Sin rol",
                Jwt = User.FindFirstValue("jwt") ?? "Sin JWT"
            };

        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(user);
    }
}