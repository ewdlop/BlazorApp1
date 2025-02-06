using BlazorApp1.Interfaces;
using BlazorApp1.Models;

public interface IUserService
{
    Task<Result<IUserData>> GetUserAsync(int id);
    Maybe<IUserData> GetUserFromCache(int id);
}
