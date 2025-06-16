using StoreApp.Shared.Interfaces.Services;

namespace StoreApp.BLL.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
