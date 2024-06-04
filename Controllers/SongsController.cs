using System.Security.Claims;
using frontendmusicshark.Models;
using frontendmusicshark.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontendmusicshark;

[Authorize(Roles = "Administrador, Usuario")]
public class SongsController(SongClientService songs,
        MusicGenreClientService musicGenres,
        FileClientService files,
        IConfiguration configuration) : Controller
{
    public async Task<IActionResult> Index(string? s)
    {
        List<Song>? list = [];
        try
        {
            list = await songs.GetAsync(s);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        if (User.FindFirstValue(ClaimTypes.Role) == "Administrador")
            ViewBag.SoloAdmin = true;

        ViewBag.Url = configuration["UrlWebAPI"];
        ViewBag.search = s;
        return View(list);
    }

    public async Task<IActionResult> Detail(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        Song? item = null;
        
        try
        {
            item = await songs.GetAsync(id);
            if (item == null) return NotFound();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
         

        return View(item);
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create()
    {
        await SongsDropDownListAsync();
        ViewBag.Url = configuration["UrlWebAPI"];
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CreateAsync(Song itemToCreate)
    {
         Console.Error.WriteLine($"LLegaIdFile: {itemToCreate.FileId}");
        if (ModelState.IsValid)
        {
            
            try
            {
                await songs.PostAsync(itemToCreate);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");

            }
        }
        ModelState.AddModelError("Nombre", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToCreate);

    }


    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> EditAsync(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        Song? itemToEdit = null;
        
        try
        {
            itemToEdit = await songs.GetAsync(id);
            if (itemToEdit == null) return NotFound();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        await SongsDropDownListAsync();
        return View(itemToEdit);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> EditAsync(int id, Song itemToEdit)
    {
       
        
        if (id != itemToEdit.SongId) return NotFound();

        ViewBag.Url = configuration["UrlWebAPI"];
        
            try
            {
                await songs.PutAsync(itemToEdit);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        
        await SongsDropDownListAsync();
        ModelState.AddModelError("Nombre", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToEdit);
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id, bool? showError = false)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        Song? itemToDelete = null;
        try
        {
            itemToDelete = await songs.GetAsync(id);
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
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                await songs.DeleteAsync(id);
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

    [AcceptVerbs("GET", "POST")]
    [Authorize(Roles = "Administrador")]
    public IActionResult ValidaPoster(string Poster)
    {
        if (Uri.IsWellFormedUriString(Poster, UriKind.Absolute) || Poster.Equals("N/A"))
            return Json(true);
        return Json(false);
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> MusicGenres(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        Song? itemToView = null;
        try
        {
            itemToView = await songs.GetAsync(id);
            if (itemToView == null) return NotFound();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        ViewData["SongId"] = itemToView?.SongId;
        return View(itemToView);
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AddMusicGenres(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        SongMusicGenre? itemToView = null;
        try
        {
            Song? song = await songs.GetAsync(id);
            if (song == null) return NotFound();

            await MusicGenresDropDownListAsync();
            itemToView = new SongMusicGenre { Song = song };

        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(itemToView);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> AddMusicGenres(int id, int musicgenreid)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        Song? song = null;
        if (ModelState.IsValid)
        {
            try
            {
                song = await songs.GetAsync(id);
                if (song == null) return NotFound();

                MusicGenre? musicGenre = await musicGenres.GetAsync(musicgenreid);
                if (musicGenre == null) return NotFound();

                await songs.PostAsync(id, musicgenreid);
                return RedirectToAction(nameof(MusicGenres), new { id });
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }
        ModelState.AddModelError("id", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        await MusicGenresDropDownListAsync();
        return View(new SongMusicGenre { Song = song });
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> RemoveMusicGenres(int id, int idMusicGenre, bool? showError = false)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        SongMusicGenre? itemToView = null;
        try
        {
            Song? song = await songs.GetAsync(id);
            if (song == null) return NotFound();

            MusicGenre? musicGenre = await musicGenres.GetAsync(idMusicGenre);
            if (musicGenre == null) return NotFound();

            itemToView = new SongMusicGenre { Song = song, MusicGenreId = idMusicGenre, Name = musicGenre.Name };

            if (showError.GetValueOrDefault())
                ViewData["ErrorMessage"] = "No ha sido posible realizar la acción. Inténtelo nuevamente.";
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }
        return View(itemToView);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> RemoveMusicGenres(int id, int musicgenreid)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            Console.Error.WriteLine($"LLegaIdFile: {id}");
            try
            {
                await songs.DeleteAsync(id, musicgenreid);
                return RedirectToAction(nameof(MusicGenres), new { id });
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }
        return RedirectToAction(nameof(MusicGenres), new { id });
    }

    private async Task MusicGenresDropDownListAsync(object? itemSeleccionado = null)
    {
        var list = await musicGenres.GetAsync();
        ViewBag.Categoria = new SelectList(list, "MusicGenreId", "Name", itemSeleccionado);
    }

    private async Task SongsDropDownListAsync(object? itemSeleccionado = null)
    {
        var list = await files.GetAsync();
        ViewBag.Archivo = new SelectList(list, "FileId", "Name", itemSeleccionado);
    }
}