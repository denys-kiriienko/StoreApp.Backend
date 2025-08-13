using StoreApp.DAL.Entities;

namespace StoreApp.DAL.Repositories.Interfaces;

public interface IProductRepository
{
    Task AddProductAsync(ProductEntity product);
    Task DeleteProductByIdAsync(int id);
    Task<IEnumerable<ProductEntity>> GetAllProductsAsync();
    Task<ProductEntity?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductEntity>> GetFilteredProductsAsync(decimal? minPrice, decimal? maxPrice, string? searchTerm);
    Task<bool> ProductExistsAsync(int id);
    Task UpdateProductAsync(ProductEntity product);
}