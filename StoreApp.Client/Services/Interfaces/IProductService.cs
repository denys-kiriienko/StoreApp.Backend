using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(int productId);

    Task<List<ProductModel>> GetAlsoLikeProductsAsync(int productId);
    Task<List<ProductModel>> GetAllProductsAsyncWithFiltersAsync(decimal? minPrice, decimal? maxPrice, string? search);
}