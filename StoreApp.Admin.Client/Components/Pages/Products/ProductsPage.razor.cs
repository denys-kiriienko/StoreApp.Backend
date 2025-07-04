using Microsoft.AspNetCore.Components;
using StoreApp.Admin.Client.Services.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.Admin.Client.Components.Pages.Products;

public partial class ProductsPage
{
    [Inject] public required IProductApiService ProductApiService { get; set; }

    private List<ProductModel>? products;

    protected override async Task OnInitializedAsync()
    {
        products = await ProductApiService.GetAllProductsAsync();
    }
}
