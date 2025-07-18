using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ProductInfo : ComponentBase
{
    [Parameter]
    public ProductModel Product { get; set; }
    
    public int SelectedColorIndex { get; set; } = 0;

    public int SelectedSizeIndex { get; set; } = 0;
}