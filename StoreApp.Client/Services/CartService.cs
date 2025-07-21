using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public class CartService(ILocalStorageService localStorageService) : ICartService
{
    private const string CartKey = "cart";
    
    public event Action<int>? OnCartItemCountChanged;

    public async Task AddToCartAsync(OrderItemModel orderItem)
    {
        if (orderItem == null)
        {
            throw new ArgumentNullException(nameof(orderItem));
        }

        var cartItems = (await GetCartItemsAsync()).ToList();
        var existingItem = cartItems.FirstOrDefault(item => item.ProductModel.Id == orderItem.ProductModel.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += orderItem.Quantity;
        }
        else
        {
            cartItems.Add(orderItem);
            OnCartItemCountChanged?.Invoke(cartItems.Count);
        }

        await localStorageService.SetItemAsync(CartKey, cartItems);
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cartItems = (await GetCartItemsAsync()).ToList();
        var itemToRemove = cartItems.FirstOrDefault(item => item.ProductModel.Id == productId);

        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            await localStorageService.SetItemAsync(CartKey, cartItems);
            OnCartItemCountChanged?.Invoke(cartItems.Count);
        }
    }

    public async Task ClearCartAsync()
    {
        await localStorageService.RemoveItemAsync(CartKey);
        OnCartItemCountChanged?.Invoke(0);
    }

    public async Task<IEnumerable<OrderItemModel>> GetCartItemsAsync()
    {
        var cartItems = await localStorageService.GetItemAsync<List<OrderItemModel>>(CartKey);
        return cartItems ?? [];
    }

    public async Task<decimal> GetTotalPriceAsync()
    {
        var cartItems = await GetCartItemsAsync();
        return cartItems.Sum(item => item.TotalPrice);
    }

    public async Task<int> GetCartItemCountAsync()
    {
        var cartItems = await GetCartItemsAsync();
        return cartItems.Count();
    }
}