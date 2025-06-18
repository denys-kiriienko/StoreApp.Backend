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
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();

        return Ok(products);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return BadRequest();
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductModel model)
    {
        var result = await _productService.AddProductAsync(model);
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] ProductModel model)
    {
        var result = await _productService.UpdateProductByIdAsync(id, model);
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var result = await _productService.DeleteProductByIdAsync(id);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }
}
