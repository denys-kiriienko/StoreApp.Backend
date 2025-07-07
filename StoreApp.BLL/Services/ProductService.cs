using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Repositories.Interfaces;
using StoreApp.Shared.Constants;
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
        if (productEntity is not null)
        {
            return _mapper.Map<ProductModel>(productEntity);
        }

        return _mapper.Map<ProductModel>(productEntity);
    }

    public async Task<bool> AddProductAsync(ProductModel product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        productEntity.ImageUrl = await SaveImageToDiskAsync(product.ImageData);

        await _productRepository.AddProductAsync(productEntity);

        return true;
    }

    public async Task<bool> UpdateProductByIdAsync(int id, ProductModel product)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(id);
        if (existingProduct is null) return false;

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;

        if (product.ImageData?.Length > 0)
        {
            DeleteImageFile(existingProduct.ImageUrl);

            existingProduct.ImageUrl = await SaveImageToDiskAsync(product.ImageData);
        }

        await _productRepository.UpdateProductAsync(existingProduct);

        return true;
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product is null) return false;

        DeleteImageFile(product.ImageUrl);

        await _productRepository.DeleteProductByIdAsync(id);
        
        return true;
    }

    private async Task<string?> SaveImageToDiskAsync(byte[]? imageData)
    {
        if (imageData is null || imageData.Length == 0)
            return null;

        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), FilesConstants.ImageFolder);
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(uploadsPath, fileName);

        await File.WriteAllBytesAsync(filePath, imageData);

        return $"/images/{fileName}";
    }

    private void DeleteImageFile(string? imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;

        var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var imagePath = Path.Combine(webRootPath, imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
