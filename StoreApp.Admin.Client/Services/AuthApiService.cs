using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StoreApp.Admin.Client.Services;
using System.Net.Http.Json;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    private readonly IJSRuntime _jsRuntime;
    public bool IsAuthenticated { get; private set; }
    public event Action<bool>? AuthStateChanged;

    public AuthApiService(
        HttpClient http,
        NavigationManager navigation,
        IJSRuntime jsRuntime)
    {
        _http = http;
        _navigation = navigation;
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", new { email, password });

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
            var result = await _http.PostAsync("api/auth/refresh-token", null);

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
        await _http.PostAsync("api/auth/logout", null);
        SetAuthenticated(false);
        _navigation.NavigateTo("/login", forceLoad: true);
    }

    private async void SetAuthenticated(bool value)
    {
        IsAuthenticated = value;
        AuthStateChanged?.Invoke(value);

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", value.ToString().ToLower());
    }

}
