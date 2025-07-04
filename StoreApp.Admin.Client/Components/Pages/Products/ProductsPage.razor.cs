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

    private void OnAddProduct()
    {
        // TODO: implement in future
        Console.WriteLine("Add clicked");
    }

    private void OnEdit(ProductModel product)
    {
        // TODO: implement in future
        Console.WriteLine($"Edit: {product.Name}");
    }

    private void OnDelete(ProductModel product)
    {
        // TODO: implement in future
        Console.WriteLine($"Delete: {product.Name}");
    } 
}
