using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace StoreApp.Admin.Client.Services;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly IJSRuntime _jsRuntime;

    public bool IsAuthenticated { get; private set; }
    public event Action<bool>? AuthStateChanged;

    public AuthApiService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { email, password });

        if (response.IsSuccessStatusCode)
        {
            SetAuthenticated(true);
            return true;
        }

        SetAuthenticated(false);
        return false;
    }

    public async Task TryAutoLoginAsync()
    {
        try
        {
            // null because we don't need to send any content for this endpoint
            var result = await _httpClient.PostAsync("api/auth/refresh-token", null);

            if (result.IsSuccessStatusCode)
            {
                SetAuthenticated(true);
            }
            else
            {
                SetAuthenticated(false);
            }
        }
        catch
        {
            SetAuthenticated(false);
        }
    }

    public async Task LogoutAsync()
    {
        // null because we don't need to send any content for this endpoint
        await _httpClient.PostAsync("api/auth/logout", null);

        SetAuthenticated(false);
        _navigationManager.NavigateTo("/login", forceLoad: true);
    }

    private async void SetAuthenticated(bool value)
    {
        IsAuthenticated = value;
        AuthStateChanged?.Invoke(value);

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", value.ToString().ToLower());
    }

}
