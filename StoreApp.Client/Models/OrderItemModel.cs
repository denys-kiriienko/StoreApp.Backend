namespace StoreApp.Client.Models;

public class OrderItemModel
{
    public required ProductModel ProductModel { get; set; }

    public required int Quantity { get; set; }

    public decimal TotalPrice => (decimal)ProductModel.CurrentPrice * Quantity;
}