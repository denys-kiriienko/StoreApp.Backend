using StoreApp.Shared.Models;

namespace StoreApp.Shared.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(UserModel user);
        Task<(string?, string?)> LoginUserAsync(UserModel user);
        Task<(string?, string?)> RefreshTokenAsync(string refreshToken);
    }
}