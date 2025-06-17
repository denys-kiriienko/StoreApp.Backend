namespace StoreApp.DAL.Entities;

public class CartItemEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public required UserEntity User { get; set; }

    public int ProductId { get; set; }
    public required ProductEntity Product { get; set; }

    public int Quantity { get; set; }
}
