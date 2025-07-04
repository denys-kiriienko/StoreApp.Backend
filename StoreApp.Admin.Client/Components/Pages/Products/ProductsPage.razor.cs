using Microsoft.AspNetCore.Components;
using StoreApp.Admin.Client.Services.Interfaces;
using StoreApp.Shared.Models;

namespace StoreApp.Admin.Client.Components.Pages.Products;

public partial class ProductsPage
{
    [Inject] public required IProductApiService ProductApiService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    private List<ProductModel>? products;

    protected override async Task OnInitializedAsync()
    {
        products = await ProductApiService.GetAllProductsAsync();
    }

    private void OnAddProduct()
    {
        NavigationManager.NavigateTo($"/product-form/");
    }

    private void OnEdit(ProductModel product)
    {
        NavigationManager.NavigateTo($"/product-form/{product.Id}");
    }

    private void OnDelete(ProductModel product)
    {
        // TODO: implement in future
        Console.WriteLine($"Delete: {product.Name}");
    } 
}
