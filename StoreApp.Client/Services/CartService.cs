using StoreApp.Client.Models;
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
            await httpClient.PostAsync(
                $"cartItems?productId={orderItem.ProductModel.Id}&quantity={orderItem.Quantity}",
                null);
            
            OnCartItemCountChanged?.Invoke(cartItems.Count);
        }

        await localStorageService.SetItemAsync(CartKey, cartItems);
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cartItems = (await GetCartItemsAsync()).ToList();
        var itemToRemove = cartItems.FirstOrDefault(item => item.ProductModel.Id == productId);

        if (itemToRemove is not null)
        {
            cartItems.Remove(itemToRemove);
            
            await httpClient.DeleteAsync($"cartItems?productId={productId}");
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
        
        if (cartItems is not null && cartItems.Any())
        {
            return cartItems;
        }

        var apiCartItems = await httpClient.GetFromJsonAsync<List<ApiCartItem>>("cartItems");
        cartItems = (apiCartItems ?? new List<ApiCartItem>())
            .Select(MapToClientCartItem)
            .ToList();

        if (cartItems is null)
        {
            cartItems = new List<OrderItemModel>();
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

        if (itemToUpdate is not null)
        {
            itemToUpdate.Quantity = quantity;
            await localStorageService.SetItemAsync(CartKey, cartItems);
            OnCartItemCountChanged?.Invoke(cartItems.Count);
            await httpClient.PutAsync($"cartItems?productId={productId}&quantity={quantity}", null);
        }

        return cartItems;
    }

    private OrderItemModel MapToClientCartItem(ApiCartItem apiCartItem)
    {
        return new OrderItemModel
        {
            Id = apiCartItem.Id,
            UserId = apiCartItem.UserId,
            Quantity = apiCartItem.Quantity,
            ProductModel = MapToClientProduct(apiCartItem.ProductModel)
        };
    }

    private ProductModel MapToClientProduct(ApiProduct apiProduct)
    {
        var apiRoot = new Uri(httpClient.BaseAddress!, "../");
        var imageSrc = string.IsNullOrWhiteSpace(apiProduct.ImageUrl)
            ? string.Empty
            : new Uri(apiRoot, apiProduct.ImageUrl.TrimStart('/')).ToString();

        var colors = apiProduct.Variants
            .Select(v => v.ColorHex)
            .Where(h => !string.IsNullOrWhiteSpace(h))
            .Distinct()
            .ToList();

        var sizes = apiProduct.Variants
            .Select(v => v.SizeName)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct()
            .ToList();

        var unitsInStock = apiProduct.Variants.Sum(v => v.UnitsInStock);

        return new ProductModel
        {
            Id = apiProduct.Id,
            Title = apiProduct.Name,
            Description = apiProduct.Description ?? string.Empty,
            CurrentPrice = (double)apiProduct.Price,
            OldPrice = null,
            Discount = null,
            ImageSrc = imageSrc,
            Images = string.IsNullOrWhiteSpace(imageSrc) ? new List<string>() : new List<string> { imageSrc },
            Colors = colors,
            Sizes = sizes,
            UnitsInStock = unitsInStock,
            Rating = 0
        };
    }

    private sealed class ApiCartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApiProduct ProductModel { get; set; }
        public int Quantity { get; set; }
    }

    private sealed class ApiProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<ApiProductVariant> Variants { get; set; } = new();
    }

    private sealed class ApiProductVariant
    {
        public int Id { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public string ColorHex { get; set; } = string.Empty;
        public string SizeName { get; set; } = string.Empty;
        public int UnitsInStock { get; set; }
        public string SKU { get; set; } = string.Empty;
    }
}