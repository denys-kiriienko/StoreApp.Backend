using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Shared.Interfaces.Services;

namespace StoreApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var items = await _cartItemService.GetCartItemsByUserIdAsync(userId);
        return Ok(items);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromQuery] int userId, [FromQuery] int productId, [FromQuery] int quantity)
    {
        var result = await _cartItemService.AddToCartAsync(userId, productId, quantity);
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateQuantity([FromQuery] int userId, [FromQuery] int productId, [FromQuery] int quantity)
    {
        var result = await _cartItemService.UpdateCartItemAsync(userId, productId, quantity);
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteItem([FromQuery] int userId, [FromQuery] int productId)
    {
        var result = await _cartItemService.DeleteCartItemAsync(userId, productId);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [Authorize]
    [HttpDelete("clear/{userId}")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        var result = await _cartItemService.ClearCartItemsByUserIdAsync(userId);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }

}
