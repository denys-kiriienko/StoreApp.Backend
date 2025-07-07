using Microsoft.AspNetCore.Components;
using StoreApp.Shared.Models;

namespace StoreApp.Admin.Client.Components.Pages.Products.Components.ProductCard;

public partial class ProductCardComponent
{
    [Inject] public required IConfiguration Configuration { get; set; }
    [Parameter] public required ProductModel Product { get; set; }
    [Parameter] public required EventCallback<ProductModel> OnEdit { get; set; }
    [Parameter] public required EventCallback<ProductModel> OnDelete { get; set; }

    private string BaseUrl = string.Empty;

    protected override void OnInitialized()
    {
        BaseUrl = Configuration["ApiSettings:BaseUrl"] ?? "";
    }
}
