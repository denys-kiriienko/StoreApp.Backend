using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;

namespace StoreApp.Client.Components.Pages.Category.Components.ProductList;

public partial class ProductListComponent
{
    [Parameter] public List<ProductModel> ProductList { get; set; } = new();
    [Parameter] public string HeaderText { get; set; } = string.Empty;
    [Parameter] public bool IsVisibleViewAllButton { get; set; } = true;
    [Parameter] public EventCallback OnToggleFilters { get; set; }
}
