using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.Data;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces.Repositories;

namespace StoreApp.DAL.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CartItemEntity?> GetCartItemAsync(int userId, int productId)
    {
        return await _context.CartItems
            .Include(c => c.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
    }

    public async Task<List<CartItemEntity>> GetCartItemsByUserIdAsync(int id)
    {
        return await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == id)
            .ToListAsync();
    }

    public async Task AddCartItemAsync(CartItemEntity cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(CartItemEntity cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCartItemAsync(CartItemEntity cartItem)
    {
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task ClearCartItemsByUserIdAsync(int userId)
    {
        var userCartItems = await _context.CartItems
            .Where(c => c.UserId == userId)
            .ToListAsync();

        _context.CartItems.RemoveRange(userCartItems);
        await _context.SaveChangesAsync();
    }
}
