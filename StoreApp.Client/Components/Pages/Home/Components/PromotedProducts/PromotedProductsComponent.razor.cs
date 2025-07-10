using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.Pages.Home.Components.PromotedProducts;

public partial class PromotedProductsComponent
{
    [Parameter] public string HeaderText { get; set; } = string.Empty;
    [Parameter] public bool IsVisibleViewAllButton { get; set; } = true;
}
