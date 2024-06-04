using frontendmusicshark.Models;

namespace frontendmusicshark.Services;

public class LogBookClientService(HttpClient client)
{
    public async Task<List<LogBook>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<LogBook>>("api/logbook");
    }
}