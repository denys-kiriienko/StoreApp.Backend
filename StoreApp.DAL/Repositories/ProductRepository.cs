using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.Data;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces.Repositories;

namespace StoreApp.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ProductEntity?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddProductAsync(ProductEntity product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductEntity product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductByIdAsync(int id)
    {
        var product = await GetProductByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }
}
