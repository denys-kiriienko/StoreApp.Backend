namespace StoreApp.DAL.Entities
{
    public class CartItemEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
