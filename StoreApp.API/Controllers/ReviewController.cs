using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.API.Extensions;
using StoreApp.Shared.Dtos.Review;
using StoreApp.Shared.Interfaces.Services;

namespace StoreApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewService service) : ControllerBase
{
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByProductIdAsync(int productId)
    {
        return Ok(await service.GetReviewsByProductIdAsync(productId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await service.GetReviewByIdAsync(id));
    }

    [Authorize]
    [HttpGet("{productId}/user")]
    public async Task<IActionResult> IsReviewedByUser(int productId)
    {
        return Ok(await service.UserHasReviewedProductAsync(User.GetUserId(), productId));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateReview dto)
    {
        await service.AddReviewAsync(dto, User.GetUserId());
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        await service.DeleteReviewAsync(id);
        return NoContent();
    }
}