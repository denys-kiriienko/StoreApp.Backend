using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Shared.Interfaces.Services;
using System.Security.Claims;

namespace StoreApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemController(ICartItemService cartItemService) : ControllerBase
{
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserIdAsync()
    {
        return Ok(await cartItemService.GetCartItemsByUserIdAsync(GetUserId()));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCartAsync([FromQuery] int productId, [FromQuery] int quantity)
    {
        return await cartItemService.AddToCartAsync(GetUserId(), productId, quantity)
            ? Ok()
            : BadRequest();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateQuantityAsync([FromQuery] int productId, [FromQuery] int quantity)
    {
        return await cartItemService.UpdateCartItemAsync(GetUserId(), productId, quantity)
            ? Ok() 
            : BadRequest();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteItemAsync([FromQuery] int productId)
    {
        return await cartItemService.DeleteCartItemAsync(GetUserId(), productId)
            ? NoContent()
            : BadRequest();
    }

    [Authorize]
    [HttpDelete("clear/{userId}")]
    public async Task<IActionResult> ClearCartAsync()
    {
        return await cartItemService.ClearCartItemsByUserIdAsync(GetUserId())
            ? NoContent()
            : BadRequest();
    }

    private int GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("User ID not found in claims.");
        }

        return userId;
    }
}
