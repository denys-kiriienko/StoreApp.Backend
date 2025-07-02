using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.Data;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Repositories.Interfaces;

namespace StoreApp.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
    {
        return await _appDbContext.Products.ToListAsync();
    }

    public async Task<ProductEntity?> GetProductByIdAsync(int id)
    {
        return await _appDbContext.Products.FindAsync(id);
    }

    public async Task AddProductAsync(ProductEntity product)
    {
        _appDbContext.Products.Add(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductEntity product)
    {
        _appDbContext.Products.Update(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteProductByIdAsync(int id)
    {
        var product = await GetProductByIdAsync(id);
        if (product == null) return;

        _appDbContext.Products.Remove(product);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _appDbContext.Products.AnyAsync(p => p.Id == id);
    }
}
