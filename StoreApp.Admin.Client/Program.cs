using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StoreApp.Admin.Client.Handlers;
using StoreApp.Admin.Client.Services;
using StoreApp.Admin.Client.Services.Interfaces;

namespace StoreApp.Admin.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped<AuthHttpHandler>();

        var baseApiUrl = builder.Configuration["ApiSettings:BaseUrl"];
        if (baseApiUrl is null)
        {
            throw new InvalidOperationException("Incorrectly configured ApiSettings: BaseUrl");
        }

        builder.Services.AddScoped(sp =>
        {
            var handler = sp.GetRequiredService<AuthHttpHandler>();
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseApiUrl)
            };
        });

        builder.Services.AddScoped<IAuthApiService, AuthApiService>();
        builder.Services.AddScoped<IProductApiService, ProductApiService>();

        await builder.Build().RunAsync();
    }
}
