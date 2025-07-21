using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ProductReview : ComponentBase
{
    [Parameter]
    public ReviewModel Review { get; set; }
}