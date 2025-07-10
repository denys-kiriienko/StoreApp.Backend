namespace StoreApp.Client.Models;

public class ProductModel
{
    public string Title { get; set; } = String.Empty;
    public double Rating { get; set; } = 0.0;
    public string CurrentPrice { get; set; } = String.Empty;
    public string? OldPrice { get; set; }
    public string? Discount { get; set; }
    public string ImageSrc { get; set; } = String.Empty;
}
