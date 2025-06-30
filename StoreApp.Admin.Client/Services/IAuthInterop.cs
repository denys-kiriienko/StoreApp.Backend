namespace StoreApp.Admin.Client.Services;

public interface IAuthInterop
{
    Task<object?> Login(string email, string password);
    Task Logout();
}