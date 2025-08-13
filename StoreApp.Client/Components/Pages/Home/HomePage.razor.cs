using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;
using StoreApp.Client.Services;

namespace StoreApp.Client.Components.Pages.Home;

public partial class HomePage
{
    [Inject] public required IProductService ProductService { get; set; }

    private List<ProductModel> NewArrivals = new();
    private List<ProductModel> TopSelling = new();

    protected override async Task OnInitializedAsync()
    {
        // TODO: implement rating system and top sales
        var products = await ProductService.GetAlsoLikeProductsAsync(0);
        NewArrivals = products.Take(4).ToList();
        TopSelling = products.Skip(4).Take(4).ToList();
    }
}
