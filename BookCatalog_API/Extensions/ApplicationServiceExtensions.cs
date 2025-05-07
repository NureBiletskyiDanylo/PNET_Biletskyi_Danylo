using BookCatalog_API.Data;
using BookCatalog_API.Data.Repositories;
using BookCatalog_API.Interfaces;
using BookCatalog_API.Services;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog_API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        
        services.AddControllers();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Default"));
        });


        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ILogRepository, LogRepository>();

        services.AddOpenApi();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
