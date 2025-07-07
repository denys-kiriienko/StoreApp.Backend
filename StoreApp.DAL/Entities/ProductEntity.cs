namespace StoreApp.DAL.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
}
