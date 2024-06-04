using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendmusicshark;

[Authorize(Roles = "Administrador")]
public class MusicGenreController(MusicGenreClientService musicGenre) : Controller
{
    public async Task<IActionResult> Index()
    {
        List<MusicGenre>? list = [];
        try
        {
            list = await musicGenre.GetAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(list);
    }

    public async Task<IActionResult> Detail(int id)
    {
        MusicGenre? item = null;
        try
        {
            item = await musicGenre.GetAsync(id);
            if (item == null) return NotFound();        
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(item);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(MusicGenre itemToCreate)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await musicGenre.PostAsync(itemToCreate);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }

        ModelState.AddModelError("Nombre", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToCreate);
    }

    public async Task<IActionResult> EditAsync(int id)
    {
        MusicGenre? itemToEdit = null;
        try
        {
            itemToEdit = await musicGenre.GetAsync(id);
            if (itemToEdit == null) return NotFound();            
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(itemToEdit);
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(int id, MusicGenre itemToEdit)
    {
        if (id != itemToEdit.MusicGenreId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await musicGenre.PutAsync(itemToEdit);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }

        ModelState.AddModelError("Nombre", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToEdit);
    }

    public async Task<IActionResult> Delete(int id, bool? showError = false)
    {
        MusicGenre? itemToDelete = null;
        try
        {
            itemToDelete = await musicGenre.GetAsync(id);
            if (itemToDelete == null) return NotFound();

            if (showError.GetValueOrDefault())
                ViewData["ErrorMessage"] = "No ha sido posible realizar la acción. Inténtelo nuevamente.";            
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(itemToDelete);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await musicGenre.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }
        return RedirectToAction(nameof(Delete), new { id, showError = true });
    }
}
