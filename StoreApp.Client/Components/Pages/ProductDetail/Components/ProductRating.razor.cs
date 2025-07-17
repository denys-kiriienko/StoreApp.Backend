using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ProductRating : ComponentBase
{
    [Parameter] public double Rating { get; set; } = 0.0;
    [Parameter] public string RatingText { get; set; } = string.Empty;
}