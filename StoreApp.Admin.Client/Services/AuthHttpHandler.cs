using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net;

namespace StoreApp.Admin.Client.Services;

public class AuthHttpHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;

    public AuthHttpHandler(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/refresh-token");
            refreshRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var refreshResponse = await base.SendAsync(refreshRequest, cancellationToken);

            if (refreshResponse.IsSuccessStatusCode)
            {
                var retryRequest = await CloneHttpRequestMessageAsync(request);
                retryRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                return await base.SendAsync(retryRequest, cancellationToken);
            }

            _navigationManager.NavigateTo("/login");
        }

        return response;
    }

    private async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        foreach (var header in request.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        if (request.Content is not null)
        {
            var content = await request.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(content);

            foreach (var header in request.Content.Headers)
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}
