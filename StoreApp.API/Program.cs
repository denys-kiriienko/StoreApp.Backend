using Microsoft.EntityFrameworkCore;
using StoreApp.BLL.MapperProvider;
using StoreApp.DAL.Data;
using StoreApp.DAL.Interfaces.Repositories;
using StoreApp.DAL.Repositories;

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

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
