using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
