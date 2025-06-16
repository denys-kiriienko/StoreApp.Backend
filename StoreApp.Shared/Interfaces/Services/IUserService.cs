using StoreApp.Shared.Models;

namespace StoreApp.BLL.Services
{
    public interface IUserService
    {
        Task<bool> DeleteUserByIdAsync(int id);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetUserByEmailAsync(string email);
        Task<UserModel?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsyncById(int id, UserModel user);
    }
}