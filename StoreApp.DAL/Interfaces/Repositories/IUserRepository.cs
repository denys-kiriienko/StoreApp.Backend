using StoreApp.DAL.Entities;

namespace StoreApp.DAL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetUserByIdAsync(int id);
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserEntity user);
        Task UpdateUserByIdAsync(UserEntity user);
        Task DeleteUserByIdAsync(int id);
        Task<bool> UserExistsAsync(int id);
    }
}