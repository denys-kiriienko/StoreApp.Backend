using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StoreApp.Admin.Client.Services;

namespace StoreApp.Admin.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri("https://localhost:7056/") });

            builder.Services.AddScoped<IAuthInterop, AuthInterop>();
            builder.Services.AddScoped<FetchInterop>();
            builder.Services.AddScoped<IAuthApiService, AuthApiService>();
            builder.Services.AddScoped<IProductApiService, ProductApiService>();

            await builder.Build().RunAsync();
        }
    }
}
