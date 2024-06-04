using frontendmusicshark.Models;

namespace frontendmusicshark.Services;

public class UserClientService(HttpClient client)
{
    public async Task<List<User>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<User>>("api/user");
    }

    public async Task<User?> GetAsync(string email)
    {
        return await client.GetFromJsonAsync<User>($"api/user/{email}");
    }

    public async Task<bool> PostAsync(UserPwd userPwd)
    {
        var response = await client.PostAsJsonAsync($"api/user", userPwd);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync(User user)
    {
        var response = await client.PutAsJsonAsync($"api/user/{user.Email}", user);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string email)
    {
        var response = await client.DeleteAsync($"api/user/{email}");
        return response.IsSuccessStatusCode;
    }
}