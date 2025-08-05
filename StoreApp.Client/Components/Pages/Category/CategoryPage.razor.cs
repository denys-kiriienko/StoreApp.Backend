using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.Category;

public partial class CategoryPage
{
    private List<ProductModel> Casual = new()
    {
        new() { ImageSrc="/images/card-item-9.png", Title="Gradient Graphic T-shirt", Rating=3.5, CurrentPrice=145 },
        new() { ImageSrc="/images/card-item-10.png", Title="Polo with Tipping Details", Rating=4.5, CurrentPrice=180 },
        new() { ImageSrc="/images/card-item-11.png", Title="Black Striped T-shirt", Rating=5.0, CurrentPrice=120, OldPrice=150, Discount=30 },
        new() { ImageSrc="/images/card-item-2.png", Title="Skinny Fit Jeans", Rating=3.5, CurrentPrice=240, OldPrice=260, Discount=20 },
        new() { ImageSrc="/images/card-item-3.png", Title="Checkered Shirt", Rating=4.5, CurrentPrice=180 },
        new() { ImageSrc="/images/card-item-4.png", Title="Sleeve Striped T-shirt", Rating=4.5, CurrentPrice=130, OldPrice=160, Discount=30 },
        new() { ImageSrc="/images/card-item-5.png", Title="Vertical Striped Shirt", Rating=5.0, CurrentPrice=212, OldPrice=232, Discount=20 },
        new() { ImageSrc="/images/card-item-6.png", Title="Courage Graphic T-shirt", Rating=4.0, CurrentPrice=145 },
        new() { ImageSrc="/images/card-item-7.png", Title="Loose Fit Bermuda Shorts", Rating=3.0, CurrentPrice=80 }
    };
}
