namespace frontendmusicshark.Services;

public class ProfileClientService(HttpClient client)
{
    public async Task<string> GetTimeAsync()
    {
        return await client.GetStringAsync($"api/time");
    }
}