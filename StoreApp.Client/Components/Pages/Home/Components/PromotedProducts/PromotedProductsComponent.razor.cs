using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.Home.Components.PromotedProducts;

public partial class PromotedProductsComponent
{
    [Parameter] public List<ProductModel> ProductList { get; set; } = new ();
    [Parameter] public string HeaderText { get; set; } = string.Empty;
    [Parameter] public bool IsVisibleViewAllButton { get; set; } = true;
}
