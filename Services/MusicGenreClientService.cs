using frontendmusicshark.Models;

namespace frontendmusicshark.Services;

public class MusicGenreClientService(HttpClient client)
{
    public async Task<List<MusicGenre>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<MusicGenre>>("api/musicgenre");
    }

    public async Task<MusicGenre?> GetAsync(int id)
    {
        return await client.GetFromJsonAsync<MusicGenre>($"api/musicgenre/{id}");
    }

    public async Task<bool> PostAsync(MusicGenre musicGenre)
    {
        var response = await client.PostAsJsonAsync($"api/musicgenre", musicGenre);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync(MusicGenre musicGenre)
    {
        var response = await client.PutAsJsonAsync($"api/musicgenre/{musicGenre.MusicGenreId}", musicGenre);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await client.DeleteAsync($"api/musicgenre/{id}");
        return response.IsSuccessStatusCode;
    }
}