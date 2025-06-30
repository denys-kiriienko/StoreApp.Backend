using StoreApp.Shared.Models;
using System.Net.Http.Json;

namespace StoreApp.Admin.Client.Services;

public class ProductApiService : IProductApiService
{
    private readonly FetchInterop _fetchInterop;
    private readonly HttpClient _http;
    private const string ApiPath = "https://localhost:7056/api/Product";

    public ProductApiService(FetchInterop fetchInterop, HttpClient http)
    {
        _fetchInterop = fetchInterop;
        _http = http;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        return await _fetchInterop.GetAsync<List<ProductModel>>(ApiPath);
    }

    public async Task<ProductModel?> GetProductByIdAsync(int id)
    {
        return await _fetchInterop.GetAsync<ProductModel>($"{ApiPath}/{id}");
    }

    public async Task<bool> AddProductAsync(ProductModel product)
    {
        var result = await _fetchInterop.PostAsync<object>(ApiPath, product);
        return result != null;
    }

    public async Task<bool> UpdateProductAsync(int id, ProductModel product)
    {
        var response = await _http.PutAsJsonAsync($"{ApiPath}/{id}", product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var response = await _http.DeleteAsync($"{ApiPath}/{id}");
        return response.IsSuccessStatusCode;
    }
}
