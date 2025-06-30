using StoreApp.Shared.Models;

namespace StoreApp.Admin.Client.Services;

public interface IProductApiService
{
    Task<List<ProductModel>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(int id);
    Task<bool> AddProductAsync(ProductModel product);
    Task<bool> UpdateProductAsync(int id, ProductModel product);
    Task<bool> DeleteProductAsync(int id);
}
