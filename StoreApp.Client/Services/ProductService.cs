using StoreApp.Client.Models;
using System.Net.Http.Json;

namespace StoreApp.Client.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    public async Task<ProductModel?> GetProductByIdAsync(int productId)
    {
        await Task.Delay(1000);
        return Task.FromResult(Mocks.Product).Result;
        
        // return await httpClient.GetFromJsonAsync<ProductModel>($"products/{productId}");
    }

    public async Task<List<ProductModel>> GetAlsoLikeProductsAsync(int productId)
    {
        // Simulating a delay for the mock data
        await Task.Delay(1000);
        return Task.FromResult(Mocks.AlsoLike).Result;

        // return await httpClient.GetFromJsonAsync<List<ProductModel>>($"products/{productId}/also-like");
    }

    public async Task<List<ReviewModel>> GetProductReviewsAsync(int productId)
    {
        // Simulating a delay for the mock data
        await Task.Delay(1000);
        return Task.FromResult(Mocks.Reviews).Result;

        // return await httpClient.GetFromJsonAsync<List<ReviewModel>>($"products/{productId}/reviews");
    }
}