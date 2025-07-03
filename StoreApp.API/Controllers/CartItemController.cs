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
    public async Task<IActionResult> GetByUserIdAsync(int userId)
    {
        return Ok(await _cartItemService.GetCartItemsByUserIdAsync(userId));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCartAsync([FromQuery] int userId, [FromQuery] int productId, [FromQuery] int quantity)
    {
        return await _cartItemService.AddToCartAsync(userId, productId, quantity)
            ? Ok()
            : BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateQuantityAsync([FromQuery] int userId, [FromQuery] int productId, [FromQuery] int quantity)
    {
        return await _cartItemService.UpdateCartItemAsync(userId, productId, quantity)
            ? Ok() 
            : BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteItemAsync([FromQuery] int userId, [FromQuery] int productId)
    {
        return(await _cartItemService.DeleteCartItemAsync(userId, productId))
            ? NoContent()
            : BadRequest();
    }

    [Authorize]
    [HttpDelete("clear/{userId}")]
    public async Task<IActionResult> ClearCartAsync(int userId)
    {
        return(await _cartItemService.ClearCartItemsByUserIdAsync(userId))
            ? NoContent()
            : BadRequest();
    }
}
