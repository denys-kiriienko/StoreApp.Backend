﻿namespace StoreApp.Admin.Client.Services.Interfaces;

public interface IAuthApiService
{
    bool IsAuthenticated { get; }
    event Action<bool>? AuthStateChanged;

    Task<bool> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task TryAutoLoginAsync();
}
