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

    private bool isLoading;
    
    protected override async Task OnParametersSetAsync()
    {
        isLoading = true;

        var productModel = await productService.GetProductByIdAsync(Id);

        if (productModel is null)
        {
            navigationManager.NavigateTo("/home");
            return;
        }

        product = productModel;
        AlsoLike = await productService.GetAlsoLikeProductsAsync(Id);
        Reviews = await reviewService.GetProductReviewsAsync(Id);

        isLoading = false;
    }

    private async Task OnWriteReviewClicked(ReviewModel review)
    {
        await reviewService.AddProductReviewAsync(Id, review);
        Reviews = await reviewService.GetProductReviewsAsync(Id);
    }
}
