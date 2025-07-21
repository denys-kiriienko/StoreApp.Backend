using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public interface ICartService
{
    event Action<int> OnCartItemCountChanged;

    Task AddToCartAsync(OrderItemModel orderItem);
    
    Task RemoveFromCartAsync(int productId);
    
    Task ClearCartAsync();
    
    Task<IEnumerable<OrderItemModel>> GetCartItemsAsync();
    
    Task<decimal> GetTotalPriceAsync();
    
    Task<int> GetCartItemCountAsync();
}