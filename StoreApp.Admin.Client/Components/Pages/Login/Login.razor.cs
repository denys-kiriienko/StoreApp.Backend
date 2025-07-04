using Microsoft.AspNetCore.Components;
using StoreApp.Admin.Client.Services.Interfaces;

namespace StoreApp.Admin.Client.Components.Pages.Login;

public partial class Login 
{
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IAuthApiService AuthApiService { get; set; }

    private string email = string.Empty;
    private string password = string.Empty;
    private bool showPassword;

    private async Task HandleSubmitAsync()
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
                NavigationManager.NavigateTo("/products");
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
