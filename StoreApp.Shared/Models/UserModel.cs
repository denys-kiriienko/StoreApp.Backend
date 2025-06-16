using StoreApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "Incorrect email format")]
        public string Email { get; set; } = null!;

        [MaxLength(32)]
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
