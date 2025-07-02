using Microsoft.AspNetCore.Components;
using StoreApp.Admin.Client.Services;

namespace StoreApp.Admin.Client.Components
{
    public partial class Navbar : ComponentBase, IDisposable
    {
        [Inject] public IAuthApiService AuthApiService { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        protected bool isAuthenticated = false;

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
            Navigation.NavigateTo("/Products");
        }

        private void NavigateToLogin()
        {
            Navigation.NavigateTo("/Login");
        }

        private async Task Logout()
        {
            await AuthApiService.LogoutAsync();
        }

        public void Dispose()
        {
            AuthApiService.AuthStateChanged -= OnAuthChanged;
        }
    }
}
