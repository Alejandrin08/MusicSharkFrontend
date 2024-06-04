using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Mvc;

namespace frontendmusicshark;

public class LogBookController(LogBookClientService logbook) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        List<LogBook>? lista = [];
        try
        {
            lista = await logbook.GetAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(lista);
    }
}