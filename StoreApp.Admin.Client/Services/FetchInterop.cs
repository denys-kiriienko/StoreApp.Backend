using Microsoft.JSInterop;
using System.Text.Json;

namespace StoreApp.Admin.Client.Services;

public class FetchInterop
{
    private readonly IJSRuntime _js;
    private IJSObjectReference? _module;

    public FetchInterop(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        _module = await _js.InvokeAsync<IJSObjectReference>("import", "./js/fetchWithCredentials.js");
    }

    public async Task<T> GetAsync<T>(string url)
    {
        if (_module == null)
            throw new InvalidOperationException("JS module not loaded");

        return await _module.InvokeAsync<T>("fetchWithCredentials", url, new { method = "GET" });
    }

    public async Task<T> PostAsync<T>(string url, object body)
    {
        if (_module == null)
            throw new InvalidOperationException("JS module not loaded");

        var options = new
        {
            method = "POST",
            headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            },
            body = JsonSerializer.Serialize(body),
            credentials = "include"
        };

        return await _module.InvokeAsync<T>("fetchWithCredentials", url, options);
    }
}

