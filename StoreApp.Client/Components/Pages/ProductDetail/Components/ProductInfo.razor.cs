using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.ProductDetail.Components;

public partial class ProductInfo : ComponentBase
{
    [Parameter]
    public ProductModel Product { get; set; }
    
    public int SelectedColorIndex { get; set; } = 0;

    public int SelectedSizeIndex { get; set; } = 0;

    [Parameter]
    public EventCallback<int> OnAddToCartClicked { get; set; }

    private int quantity = 1;

    private void OnAddToCart()
    {
        if (OnAddToCartClicked.HasDelegate)
        {
            OnAddToCartClicked.InvokeAsync(quantity);
        }
    }
}