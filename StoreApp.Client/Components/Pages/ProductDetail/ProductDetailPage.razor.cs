using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;
using StoreApp.Client.Services;

namespace StoreApp.Client.Components.Pages.ProductDetail;

public partial class ProductDetailPage(
    IProductService productService,
    IReviewService reviewService,
    NavigationManager navigationManager) : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    private ProductModel product;

    private List<ProductModel> AlsoLike;

    private List<ReviewModel> Reviews;

    protected override async Task OnInitializedAsync()
    {
        var productModel = await productService.GetProductByIdAsync(Id);
        
        if(productModel is null)
        {
            navigationManager.NavigateTo("/home");
        }
        else
        {
            product = productModel;
        }

        AlsoLike = await productService.GetAlsoLikeProductsAsync(Id);
        Reviews = await reviewService.GetProductReviewsAsync(Id);
    }

    private async Task OnWriteReviewClicked(ReviewModel review)
    {
        await reviewService.AddProductReviewAsync(Id, review);
        Reviews = await reviewService.GetProductReviewsAsync(Id);
    }
}
