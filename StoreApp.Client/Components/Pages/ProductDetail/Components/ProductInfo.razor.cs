using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ProductInfo : ComponentBase
{
    [Parameter]
    public ProductModel Product { get; set; } = new()
    {
        ImageSrc = "/images/card-item-1.png",
        Title = "T-shirt with Tape Details",
        Rating = 4.5,
        CurrentPrice = "$120",
        OldPrice = "$150",
        Discount = "20%",
        Description = "This graphic t-shirt which is perfect for any occasion. Crafted from a soft and breathable fabric, it offers superior comfort and style.",
        UnitsInStock = 10,
        Images =
        [
            "/images/card-item-1.png",
            "/images/card-item-3.png",
            "/images/card-item-4.png"
        ],
        Colors = ["#000000", "#ff0000", "#00ff00", "#0000ff"],
        Sizes = ["Small", "Medium", "Large", "X-Large"]
    };
    
    public int SelectedColorIndex { get; set; } = 0;

    public int SelectedSizeIndex { get; set; } = 0;
}