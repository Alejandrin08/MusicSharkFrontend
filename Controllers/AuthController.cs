using System.Security.Claims;
using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendmusicshark;

public class AuthController(AuthClientService auth, UserClientService users) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync(Login model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var token = await auth.GetTokenAsync(model.Email, model.Password);
                var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, token.Email),
                        new(ClaimTypes.GivenName, token.Name),
                        new("jwt", token.Jwt),
                        new(ClaimTypes.Role, token.Role),
                    };
                auth.LogInAsync(claims);
                return RedirectToAction("Index", "Songs");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
                ModelState.AddModelError("Email", "Credenciales no válidas. Inténtelo nuevamente.");
            }
        }
        return View(model);
    }

    [Authorize(Roles = "Administrador, Usuario")]
    public async Task<IActionResult> SalirAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Auth");
    }

    public IActionResult NewUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewUserAsync(UserPwd itemToCreate)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await users.PostAsync(itemToCreate);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }
        ModelState.AddModelError("Email", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToCreate);
    }

}