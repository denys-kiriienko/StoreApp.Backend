using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ReviewList : ComponentBase
{
    [Parameter]
    public List<ReviewModel> Reviews { get; set; } = [];
}