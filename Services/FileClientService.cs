using System.Net.Http.Headers;
using frontendmusicshark.Models;
using File = frontendmusicshark.Models.File;

namespace frontendmusicshark.Services;

public class FileClientService(HttpClient client)
{
    public async Task<List<File>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<File>>("api/file");
    }

    public async Task<File?> GetAsync(int id)
    {
        return await client.GetFromJsonAsync<File>($"api/file/{id}/details");
    }

    public async Task<bool> PostAsync(Upload files)
    {
        var memoryStream = new MemoryStream();
        await files.Image.CopyToAsync(memoryStream);
        var Contenido = memoryStream.ToArray();
        memoryStream.Close();
        var fileContent = new ByteArrayContent(Contenido);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(files.Image.ContentType);
        using var form = new MultipartFormDataContent
        {
            { fileContent, "file", files.Image.FileName! }
        };

        var response = await client.PostAsync($"api/file", form);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync(Upload files)
    {
        var memoryStream = new MemoryStream();
        await files.Image.CopyToAsync(memoryStream);
        var Contenido = memoryStream.ToArray();
        memoryStream.Close();
        var fileContent = new ByteArrayContent(Contenido);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(files.Image.ContentType);
        using var form = new MultipartFormDataContent
        {
            { fileContent, "file", files.Image.FileName! }
        };

        var response = await client.PutAsync($"api/file/{files.FileId}", form);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await client.DeleteAsync($"api/file/{id}");
        return response.IsSuccessStatusCode;
    }
}