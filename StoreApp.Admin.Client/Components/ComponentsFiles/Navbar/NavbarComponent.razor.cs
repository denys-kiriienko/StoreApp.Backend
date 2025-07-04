using Microsoft.AspNetCore.Components;
using StoreApp.Admin.Client.Services;

namespace StoreApp.Admin.Client.Components.ComponentsFiles.Navbar;

public partial class NavbarComponent : IDisposable
{
    [Inject] public required IAuthApiService AuthApiService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        AuthApiService.AuthStateChanged += OnAuthChanged;
        StateHasChanged();
    }

    private void OnAuthChanged(bool auth)
    {
        InvokeAsync(StateHasChanged);
    }

    private void NavigateToProducts()
    {
        NavigationManager.NavigateTo("/Products");
    }

    private void NavigateToLogin()
    {
        NavigationManager.NavigateTo("/Login");
    }

    private Task LogoutAsync()
    {
        return AuthApiService.LogoutAsync();
    }

    public void Dispose()
    {
        AuthApiService.AuthStateChanged -= OnAuthChanged;
    }
}
