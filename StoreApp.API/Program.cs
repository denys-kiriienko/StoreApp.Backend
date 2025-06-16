using Microsoft.EntityFrameworkCore;
using StoreApp.BLL.MapperProvider;
using StoreApp.BLL.Services;
using StoreApp.DAL.Data;
using StoreApp.DAL.Interfaces.Repositories;
using StoreApp.DAL.Repositories;
using StoreApp.Shared.Interfaces.Services;

namespace StoreApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string"
                + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
