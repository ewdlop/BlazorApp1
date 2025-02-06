using BlazorApp1.Interfaces;
using BlazorApp1.Models;

namespace BlazorApp1.Services;

public class UserService : IUserService
{
    protected readonly Dictionary<int, IUserData> _cache = new();
    protected readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<IUserData>> GetUserAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                IUserData? user = await response.Content.ReadFromJsonAsync<IUserData>();
                return user is not null
                    ? Result<IUserData>.Success(user)
                    : Result<IUserData>.Failure("User not found");
            }
            return Result<IUserData>.Failure($"Error: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return Result<IUserData>.Failure(ex.Message);
        }
    }

    public Maybe<IUserData> GetUserFromCache(int id)
    {
        return _cache.TryGetValue(id, out var user)
            ? Maybe<IUserData>.Some(user)
            : Maybe<IUserData>.None();
    }
}
