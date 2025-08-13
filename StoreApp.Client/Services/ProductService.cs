using System.Net.Http.Json;
using System.Linq;
using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    public async Task<ProductModel?> GetProductByIdAsync(int productId)
    {
        var apiProduct = await httpClient.GetFromJsonAsync<ApiProduct>($"product/{productId}");
        return apiProduct is null ? null : MapToClientModel(apiProduct, httpClient.BaseAddress!);
    }

    public async Task<List<ProductModel>> GetAlsoLikeProductsAsync(int productId)
    {
        var apiProducts = await httpClient.GetFromJsonAsync<List<ApiProduct>>("product") ?? new();
        return apiProducts.Select(p => MapToClientModel(p, httpClient.BaseAddress!)).ToList();
    }

    private static ProductModel MapToClientModel(ApiProduct apiProduct, Uri baseAddress)
    {
        var apiRoot = new Uri(baseAddress, "../");
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