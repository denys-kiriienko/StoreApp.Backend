using StoreApp.Shared.Enums;

namespace StoreApp.DAL.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    }
}
