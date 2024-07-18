using LoginFront.models;

namespace LoginFront.Clients;

public class UserClient(HttpClient httpClient)
{
    public async Task<UserSummary[]> GetUsersAsync()
     => await httpClient.GetFromJsonAsync<UserSummary[]>("Logins") ?? [];

    public async Task AddUserAsync(UserSummary user)
        => await httpClient.PostAsJsonAsync("Logins", user);

    public async Task UpdateUserAsync(UserSummary updateUser)
        => await httpClient.PutAsJsonAsync($"Logins/{updateUser.Id}", updateUser);

    public async Task DeleteUserAsync(int id)
        => await httpClient.DeleteAsync($"Logins/{id}");

    public async Task<UserSummary> GetUserAsync(int id)
        => await httpClient.GetFromJsonAsync<UserSummary>($"Logins/{id}") ??
           throw new Exception("not find : " + id);
}