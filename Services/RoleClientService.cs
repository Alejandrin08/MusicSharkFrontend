using frontendmusicshark.Models;

namespace frontendmusicshark.Services;

public class RoleClientService(HttpClient client)
{
    public async Task<List<Role>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<Role>>("api/role");
    }
}