using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public interface IProductService
{
    Task<ProductModel?> GetProductByIdAsync(int productId);

    Task<List<ProductModel>> GetAlsoLikeProductsAsync(int productId);

    Task<List<ReviewModel>> GetProductReviewsAsync(int productId);
}