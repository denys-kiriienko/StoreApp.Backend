using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail;

public partial class ProductDetailPage : ComponentBase
{
    private ProductModel product = new()
    {
        ImageSrc = "/images/card-item-1.png",
        Title = "T-shirt with Tape Details",
        Rating = 4.5,
        CurrentPrice = "$120",
        OldPrice = "$150",
        Discount = "20%",
        Images = new List<string>
        {
            "/images/card-item-1.png",
            "/images/card-item-3.png",
            "/images/card-item-4.png"
        }
    };
}
