using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Shared.Models;
using StoreApp.Shared.Services;

namespace StoreApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _productService.GetAllProductsAsync());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return product is not null 
            ? Ok(product) 
            : BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] ProductModel model)
    {
        return(await _productService.AddProductAsync(model))
            ? Ok()
            : BadRequest();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateByIdAsync(int id, [FromBody] ProductModel model)
    {
        return(await _productService.UpdateProductByIdAsync(id, model))
            ? Ok()
            : BadRequest();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(int id)
    {
        return(await _productService.DeleteProductByIdAsync(id))
            ? NoContent()
            : BadRequest();
    }
}
