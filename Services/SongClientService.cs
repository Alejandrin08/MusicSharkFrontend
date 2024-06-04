using frontendmusicshark.Models;

namespace frontendmusicshark.Services;

public class SongClientService(HttpClient client)
{
    public async Task<List<Song>?> GetAsync(string? search)
    {
        return await client.GetFromJsonAsync<List<Song>>($"api/song?s={search}");
    }

    public async Task<Song?> GetAsync(int id)
    {
        return await client.GetFromJsonAsync<Song>($"api/song/{id}");
    }

    public async Task<bool> PostAsync(Song song)
    {
        var songData = new
        {
            title = song.Title,
            artist = song.Artist,
            album = song.Album,
            duration = song.Duration,
            fileid = song.FileId
        };
        var response = await client.PostAsJsonAsync($"api/song", songData);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync(Song song)
    {
        var songData = new
        {
            id = song.SongId,
            title = song.Title,
            artist = song.Artist,
            album = song.Album,
            duration = song.Duration
        };
        var response = await client.PutAsJsonAsync($"api/song/{song.SongId}", songData);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await client.DeleteAsync($"api/song/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PostAsync(int id, int musicgenreid)
    {
        var response = await client.PostAsJsonAsync($"api/song/{id}/musicgenre", new { musicgenreid });

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id, int musicgenreid)
    {
        var response = await client.DeleteAsync($"api/song/{id}/musicgenre/{musicgenreid}");
        return response.IsSuccessStatusCode;
    }
}