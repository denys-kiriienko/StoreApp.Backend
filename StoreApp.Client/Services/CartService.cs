using StoreApp.Client.Models;
using StoreApp.Client;
using System.Net.Http.Json;

namespace StoreApp.Client.Services;

public class CartService(
    ILocalStorageService localStorageService,
    HttpClient httpClient) : ICartService
{
    private const string CartKey = "cart";
    
    public event Action<int>? OnCartItemCountChanged;

    public async Task AddToCartAsync(OrderItemModel orderItem)
    {
        if (orderItem == null)
        {
            throw new ArgumentNullException(nameof(orderItem));
        }

        var cartItems = await ChangeCartItemQuantity(orderItem.ProductModel.Id, orderItem.Quantity);
        var existingItem = cartItems.FirstOrDefault(item => item.ProductModel.Id == orderItem.ProductModel.Id);

        if (existingItem is null)
        {
            cartItems.Add(orderItem);
            await httpClient.PostAsJsonAsync("cartItems", new
            {
                productId = existingItem.ProductModel.Id,
                quantity = existingItem.Quantity
            });
            
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
            
            await httpClient.DeleteAsync($"cartItems/{productId}");
            await localStorageService.SetItemAsync(CartKey, cartItems);
            
            OnCartItemCountChanged?.Invoke(cartItems.Count);
        }
    }

    public async Task ClearCartAsync()
    {
        await localStorageService.RemoveItemAsync(CartKey);
        await httpClient.DeleteAsync("cartItems/clear");

        OnCartItemCountChanged?.Invoke(0);
    }

    public async Task<IEnumerable<OrderItemModel>> GetCartItemsAsync()
    {
        var cartItems = await localStorageService.GetItemAsync<List<OrderItemModel>>(CartKey);
        
        if (cartItems != null && cartItems.Any())
        {
            return cartItems;
        }

        cartItems = await httpClient.GetFromJsonAsync<List<OrderItemModel>>("cartItems");

        if (cartItems == null || !cartItems.Any())
        {
            // Seed with a couple of mock items for development/demo
            cartItems =
            [
                new OrderItemModel { ProductModel = Mocks.Products[0], Quantity = 1 },
                new OrderItemModel { ProductModel = Mocks.Products[1], Quantity = 2 },
            ];
        }

        await localStorageService.SetItemAsync(CartKey, cartItems);
        OnCartItemCountChanged?.Invoke(cartItems.Count);

        return cartItems;
    }

    public async Task<decimal> GetTotalPriceAsync()
    {
        var cartItems = await GetCartItemsAsync();
        return cartItems.Sum(item => (decimal)item.ProductModel.CurrentPrice * item.Quantity);
    }

    public async Task<int> GetCartItemCountAsync()
    {
        var cartItems = await GetCartItemsAsync();
        return cartItems.Count();
    }

    public async Task ChangeCartItemQuantityAsync(int productId, int quantity)
    {
        await ChangeCartItemQuantity(productId, quantity);
    }

    private async Task<List<OrderItemModel>> ChangeCartItemQuantity(int productId, int quantity)
    {
        var cartItems = (await GetCartItemsAsync()).ToList();
        var itemToUpdate = cartItems.FirstOrDefault(item => item.ProductModel.Id == productId);

        if (itemToUpdate != null)
        {
            itemToUpdate.Quantity = quantity;
            await localStorageService.SetItemAsync(CartKey, cartItems);
            OnCartItemCountChanged?.Invoke(cartItems.Count);
        }

        return cartItems;
    }
}