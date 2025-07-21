using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;
using StoreApp.Client.Services;

namespace StoreApp.Client.Components.Pages.ProductDetail;

public partial class ProductDetailPage(IProductService productService, NavigationManager navigationManager) : ComponentBase
{
    [Parameter]
    public int? Id { get; set; }

    private ProductModel product;

    private List<ProductModel> AlsoLike;

    private List<ReviewModel> Reviews;
    
    protected override async Task OnInitializedAsync()
    {
        if (Id is null)
        {
            navigationManager.NavigateTo("/home");
            return;
        }

        var productModel = await productService.GetProductByIdAsync(Id.Value);
        
        if(productModel is null)
        {
            navigationManager.NavigateTo("/home");
        }
        else
        {
            product = productModel;
        }

        AlsoLike = await productService.GetAlsoLikeProductsAsync(1);
        Reviews = await productService.GetProductReviewsAsync(1);
    }
}
