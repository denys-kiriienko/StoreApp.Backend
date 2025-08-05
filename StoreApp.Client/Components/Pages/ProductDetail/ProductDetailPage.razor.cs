using Microsoft.AspNetCore.Components;
using StoreApp.Client.Models;
using StoreApp.Client.Services;

namespace StoreApp.Client.Components.Pages.ProductDetail;

public partial class ProductDetailPage()
{
    [Inject] public required IProductService ProductService { get; set; }
    [Inject] public required IReviewService ReviewService { get; set; }
    [Inject] public required ICartService CartService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    [Parameter] public int Id { get; set; }
    
    private ProductModel? _product;
    private List<ProductModel>? _alsoLike;
    private List<ReviewModel>? _reviews;
    private bool _isLoading;
    
    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;

        var productModel = await ProductService.GetProductByIdAsync(Id);

        if (productModel is null)
        {
            NavigationManager.NavigateTo("/home");
            return;
        }

        _product = productModel;
        _alsoLike = await ProductService.GetAlsoLikeProductsAsync(Id);
        _reviews = await ReviewService.GetProductReviewsAsync(Id);

        _isLoading = false;
    }

    private async Task OnAddToCartClicked(int quantity)
    {
        if (quantity <= 0)
        {
            return;
        }

        var cartItem = new OrderItemModel
        {
            Quantity = quantity,
            ProductModel = _product,
        };

        await CartService.AddToCartAsync(cartItem);
    }

    private async Task OnWriteReviewClicked(ReviewModel review)
    {
        await ReviewService.AddProductReviewAsync(Id, review);
        _reviews = await ReviewService.GetProductReviewsAsync(Id);
    }
}
