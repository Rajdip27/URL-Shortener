using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using UrlShortener.Application.Commands;
using UrlShortener.Application.Validators;
using UrlShortener.Infrastructure.Caching;
using UrlShortener.Infrastructure.Persistence;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Infrastructure.Utilities;

namespace UrlShortener.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShortenerServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Use SQL Server connection string (update your appsettings.json accordingly)
        var connectionString = configuration.GetConnectionString("SqlServer");

        // Replace UseSqlite with UseSqlServer here
        services.AddDbContext<ShortenerDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateShortLinkCommand)));

        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();

        var redisConn = configuration.GetValue<string>("Redis:Connection") ?? "localhost:6379";
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConn));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateShortLinkValidator>();

        services.AddSingleton<Base62Encoder>();

        return services;
    }
}
