using Microsoft.AspNetCore.Components;
using StoreApp.Shared.Models;

namespace StoreApp.Admin.Client.Components.Pages.Products.Components.ProductCard;

public partial class ProductCardComponent
{
    [Parameter] public required ProductModel Product { get; set; }
    [Parameter] public required EventCallback<ProductModel> OnEdit { get; set; }
    [Parameter] public required EventCallback<ProductModel> OnDelete { get; set; }
}
