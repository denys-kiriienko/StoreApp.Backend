using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StoreApp.BLL.MapperProvider;
using StoreApp.BLL.Options;
using StoreApp.BLL.Services;
using StoreApp.DAL.Data;
using StoreApp.DAL.Interfaces.Repositories;
using StoreApp.DAL.Repositories;
using StoreApp.Shared.Interfaces.Services;
using StoreApp.Shared.Services;

namespace StoreApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenApi();
            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string"
                + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.Configure<JwtOptions>(builder.Configuration
                .GetSection(nameof(JwtOptions)));

            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("StoreApp API")
                        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
                });

                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/scalar");
                    return Task.CompletedTask;
                });
            }

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
