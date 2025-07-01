using Microsoft.AspNetCore.Components;
using StoreApp.Admin.Client.Services;

namespace StoreApp.Admin.Client.Pages.Login
{
    public partial class Login 
    {
        [Inject] public NavigationManager Navigation {  get; set; } = default!;
        [Inject] public IAuthApiService AuthApiService { get; set; } = default!;

        private string email = string.Empty;
        private string password = string.Empty;
        private bool showPassword = false;

        private async Task HandleSubmit()
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Email and password cannot be empty");
                return;
            }

            try
            {
                var success = await AuthApiService.LoginAsync(email, password);
                if (success)
                {
                    Navigation.NavigateTo("/products");
                }
                else
                {
                    Console.WriteLine("Login failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
            }
        }

        private void TogglePasswordVisibility()
        {
            showPassword = !showPassword;
        }
    }
}
