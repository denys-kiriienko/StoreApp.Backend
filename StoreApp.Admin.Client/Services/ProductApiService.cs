﻿using StoreApp.Shared.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using StoreApp.Admin.Client.Services.Interfaces;

namespace StoreApp.Admin.Client.Services;

public class ProductApiService : IProductApiService
{
    private readonly HttpClient _httpClient;
    private const string ApiPath = "api/Product";

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, ApiPath);
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ProductModel>>() ?? new();
    }

    public async Task<ProductModel?> GetProductByIdAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiPath}/{id}");
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductModel>();
    }

    public async Task<bool> AddProductAsync(ProductModel product)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, ApiPath)
        {
            Content = JsonContent.Create(product)
        };
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProductAsync(int id, ProductModel product)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{ApiPath}/{id}")
        {
            Content = JsonContent.Create(product)
        };
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{ApiPath}/{id}");
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}
