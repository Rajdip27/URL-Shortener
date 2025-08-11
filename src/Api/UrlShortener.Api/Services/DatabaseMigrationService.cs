using Microsoft.EntityFrameworkCore;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Api.Services;

public class DatabaseMigrationService
{
    private readonly IServiceProvider _provider;
    public DatabaseMigrationService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task MigrateAsync()
    {
        using var scope = _provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ShortenerDbContext>();
        await db.Database.MigrateAsync();
    }
}
