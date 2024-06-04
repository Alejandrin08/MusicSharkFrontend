using Microsoft.AspNetCore.Mvc;

namespace frontendmusicshark;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error([FromServices] IHostEnvironment hostEnvironment, int? statusCode)
    {
        if (statusCode == 404)
        {
            return View("404");
        }

        return View("Error");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
