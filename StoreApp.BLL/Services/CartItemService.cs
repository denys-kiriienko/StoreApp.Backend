using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces.Repositories;
using StoreApp.Shared.Interfaces.Services;
using StoreApp.Shared.Models;

namespace StoreApp.BLL.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IMapper _mapper;

    public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper)
    {
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
    }

    public async Task<List<CartItemModel>> GetCartItemsByUserIdAsync(int userId)
    {
        var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);

        return _mapper.Map<List<CartItemModel>>(cartItems);
    }

    public async Task<bool> AddToCartAsync(int userId, int productId, int quantity)
    {
        var existingItem = await _cartItemRepository.GetCartItemAsync(userId, productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            await _cartItemRepository.UpdateCartItemAsync(existingItem);
        }
        else
        {
            var newItem = new CartItemEntity
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };
            
            await _cartItemRepository.AddCartItemAsync(newItem);
        }

        return true;
    }

    public async Task<bool> DeleteCartItemAsync(int userId, int productId)
    {
        var cartItem = await _cartItemRepository.GetCartItemAsync(userId, productId);
        if (cartItem == null)
        {
            return false;
        }

        await _cartItemRepository.DeleteCartItemAsync(cartItem);

        return true;
    }

    public async Task<bool> UpdateCartItemAsync(int userId, int productId, int quantity)
    {
        var cartItem = await _cartItemRepository.GetCartItemAsync(userId, productId);
        if (cartItem == null)
        {
            return false;
        } 
        
        cartItem.Quantity = quantity;
        
        await _cartItemRepository.UpdateCartItemAsync(cartItem);

        return true;
    }

    public async Task<bool> ClearCartItemsByUserIdAsync(int userId)
    {
        await _cartItemRepository.ClearCartItemsByUserIdAsync(userId);

        return true;
    }
}
