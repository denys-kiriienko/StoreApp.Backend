using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ProductReview : ComponentBase
{
    [Parameter]
    public ReviewModel Review { get; set; } = new()
    {
        UserName = "John Doe",
        Rating = 4.5,
        Comment = "Great product! Highly recommend.",
        CreatedAt = DateTime.Now.AddDays(-2)
    };
}