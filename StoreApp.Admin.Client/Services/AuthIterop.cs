using Microsoft.JSInterop;

namespace StoreApp.Admin.Client.Services;

public class AuthInterop : IAuthInterop
{
    private readonly IJSRuntime _js;

    public AuthInterop(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<object?> Login(string email, string password)
    {
        return await _js.InvokeAsync<object>("login", email, password);
    }

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("logout");
    }
}
