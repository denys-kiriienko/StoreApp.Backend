using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    public Task<ProductModel?> GetProductByIdAsync(int productId)
    {
        ProductModel? product = Mocks.Products.FirstOrDefault(p => p.Id == productId);
        return Task.FromResult(product);
    }

    public async Task<List<ProductModel>> GetAlsoLikeProductsAsync(int productId)
    {
        // Simulating a delay for the mock data
        await Task.Delay(1000);
        return Task.FromResult(Mocks.Products).Result;

        // return await httpClient.GetFromJsonAsync<List<ProductModel>>($"products/{productId}/also-like");
    }
}