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
        Description = "This graphic t-shirt which is perfect for any occasion. Crafted from a soft and breathable fabric, it offers superior comfort and style.",
        UnitsInStock = 10,
        Images =
        [
            "/images/card-item-1.png",
            "/images/card-item-3.png",
            "/images/card-item-4.png"
        ],
        Colors = ["#4F4631", "#314F4A", "#31344F"],
        Sizes = ["Small", "Medium", "Large", "X-Large"]
    };

    private List<ProductModel> AlsoLike =
    [
        new()
        {
            ImageSrc = "/images/card-item-5.png", Title = "Vertical Striped Shirt", Rating = 5.0, CurrentPrice = "$212",
            OldPrice = "$232", Discount = "-20%"
        },
        new()
        {
            ImageSrc = "/images/card-item-6.png", Title = "Courage Graphic T-shirt", Rating = 4.0, CurrentPrice = "$145"
        },
        new()
        {
            ImageSrc = "/images/card-item-7.png", Title = "Loose Fit Bermuda Shorts", Rating = 3.0, CurrentPrice = "$80"
        },
        new()
        {
            ImageSrc = "/images/card-item-8.png", Title = "Faded Skinny Jeans", Rating = 4.5, CurrentPrice = "$210"
        }
    ];
    
    private List<ReviewModel> Reviews =
    [
        new()
        {
            UserName = "John Doe", Rating = 5, Comment = "Great quality and fit!", CreatedAt = DateTime.Now.AddDays(-2)
        },
        new()
        {
            UserName = "Jane Smith", Rating = 4, Comment = "Very comfortable, but a bit pricey.",
            CreatedAt = DateTime.Now.AddDays(-5)
        },
        new()
        {
            UserName = "Alice Johnson", Rating = 3, Comment = "Average product, nothing special.",
            CreatedAt = DateTime.Now.AddDays(-10)
        }
    ];
}
