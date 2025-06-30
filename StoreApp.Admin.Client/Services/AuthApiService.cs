using Microsoft.JSInterop;

namespace StoreApp.Admin.Client.Services;

public class AuthApiService : IAuthApiService
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _module;

    public event Action<bool>? AuthStateChanged;

    private const string AuthStorageKey = "isAuthenticated";

    public bool IsAuthenticated { get; private set; }

    public AuthApiService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        _module = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/auth.js");

        var storedAuth = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AuthStorageKey);
        IsAuthenticated = storedAuth == "true";
        AuthStateChanged?.Invoke(IsAuthenticated);
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        if (_module is null) return false;

        try
        {
            await _module.InvokeVoidAsync("login", email, password);
            await SetAuthenticatedAsync(true);
            return true;
        }
        catch
        {
            await SetAuthenticatedAsync(false);
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        if (_module is null) return;

        await _module.InvokeVoidAsync("logout");
        await SetAuthenticatedAsync(false);
    }


    public async Task RefreshTokenAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("refreshToken");
            await SetAuthenticatedAsync(true);
        }
        catch
        {
            await SetAuthenticatedAsync(false);
        }
    }

    private async Task SetAuthenticatedAsync(bool isAuth)
    {
        IsAuthenticated = isAuth;
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthStorageKey, isAuth ? "true" : "false");
        AuthStateChanged?.Invoke(IsAuthenticated);
    }
}
