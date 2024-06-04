using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Mvc;
using File = frontendmusicshark.Models.File;

namespace frontendmusicshark;

public class FileController(FileClientService files, IConfiguration configuration) : Controller
{
    public async Task<IActionResult> Index()
    {
        List<File>? list = [];
        try
        {
            list = await files.GetAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        ViewBag.Url = configuration["UrlWebAPI"];
        return View(list);
    }

    public async Task<IActionResult> Details(int id)
    {
        File? item = null;
        try
        {
            item = await files.GetAsync(id);
            if (item == null) return NotFound();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        ViewBag.Url = configuration["UrlWebAPI"];
        return View(item);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Upload itemToCreate)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                if ((itemToCreate.Image.Length / 1024) > 100)
                {
                    ModelState.AddModelError("Image", $"El archivo de {itemToCreate.Image.Length / 1024} KB supera el tamaño máximo permitido.");
                    return View(itemToCreate);
                }
                if (itemToCreate.Image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", $"El archivo {itemToCreate.Image.FileName} no tiene una extensión permitida.");
                    return View(itemToCreate);
                }

                await files.PostAsync(itemToCreate);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }

        ModelState.AddModelError("Image", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToCreate);
    }

    public async Task<IActionResult> EditAsync(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        try
        {
            File? itemToEdit = await files.GetAsync(id);
            ViewBag.FileId = itemToEdit?.FileId;
            ViewBag.Nombre = itemToEdit?.Name;
            if (itemToEdit == null) return NotFound();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(int id, Upload itemToEdit)
    {
        if (id != itemToEdit.FileId) return NotFound();

        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                if ((itemToEdit.Image.Length / 1024) > 100)
                {
                    ModelState.AddModelError("Image", $"El archivo de {itemToEdit.Image.Length / 1024} KB supera el tamaño máximo permitido.");
                    return View(itemToEdit);
                }
                if (itemToEdit.Image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", $"El archivo {itemToEdit.Image.FileName} no tiene una extensión permitida.");
                    return View(itemToEdit);
                }

                await files.PutAsync(itemToEdit);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }

        ModelState.AddModelError("Image", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToEdit);
    }

    public async Task<IActionResult> Delete(int id, bool? showError = false)
    {
        File? itemToDelete = null;
        try
        {
            itemToDelete = await files.GetAsync(id);
            if (itemToDelete == null) return NotFound();

            if (showError.GetValueOrDefault())
                ViewData["ErrorMessage"] = "No ha sido posible realizar la acción. Inténtelo nuevamente.";            
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        ViewBag.Url = configuration["UrlWebAPI"];
        return View(itemToDelete);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                await files.DeleteAsync(id);
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