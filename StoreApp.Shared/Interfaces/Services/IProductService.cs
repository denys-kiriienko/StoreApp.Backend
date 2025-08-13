using StoreApp.Shared.Models;

namespace StoreApp.Shared.Services;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductModel>> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string? searchTerm);
    Task<bool> AddProductAsync(ProductModel product);
    Task<bool> UpdateProductByIdAsync(int id, ProductModel product);
    Task<bool> DeleteProductByIdAsync(int id);
}