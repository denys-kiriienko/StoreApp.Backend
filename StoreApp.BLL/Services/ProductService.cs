using AutoMapper;
using StoreApp.DAL.Interfaces.Repositories;
using StoreApp.Shared.Models;
using StoreApp.Shared.Services;

namespace StoreApp.BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
    {
        var productEntities = await _productRepository.GetAllProductsAsync();

        return _mapper.Map<IEnumerable<ProductModel>>(productEntities);
    }

    public async Task<ProductModel?> GetProductByIdAsync(int id)
    {
        var productEntity = await _productRepository.GetProductByIdAsync(id);
        if (productEntity != null)
        {
            return _mapper.Map<ProductModel>(productEntity);
        }

        return null;
    }

    public async Task<bool> UpdateProductByIdAsync(int id, ProductModel product)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return false;
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.ImageData = product.ImageData;

        await _productRepository.UpdateProductAsync(existingProduct);

        return true;
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        if (!await _productRepository.ProductExistsAsync(id))
        {
            return false;
        }

        await _productRepository.DeleteProductByIdAsync(id);

        return true;
    }
}
