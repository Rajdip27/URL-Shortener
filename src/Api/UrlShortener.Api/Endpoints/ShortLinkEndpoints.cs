using MediatR;
using UrlShortener.Application.Commands;
using UrlShortener.Application.Queries;
using UrlShortener.Infrastructure.Caching;
using UrlShortener.Infrastructure.Repositories;

namespace UrlShortener.Api.Endpoints;

public static class ShortLinkEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/shorten", CreateShortLink);
        app.MapGet("/{shortCode}", RedirectShortLink);
        app.MapGet("/api/analytics/{shortCode}", GetAnalytics);
    }

    private static async Task<IResult> CreateShortLink(CreateShortLinkCommand cmd, IMediator med)
    {
        var result = await med.Send(cmd);
        return Results.Created($"/api/shorten/{result.ShortCode}", result);
    }

    private static async Task<IResult> RedirectShortLink(string shortCode, IMediator med, IRedisCacheService cache)
    {
        var cached = await cache.GetAsync<string>($"short:{shortCode}");
        if (!string.IsNullOrEmpty(cached))
        {
            _ = cache.IncrementAsync($"count:{shortCode}");
            return Results.Redirect(cached);
        }

        var original = await med.Send(new GetOriginalUrlQuery { ShortCode = shortCode });
        if (original == null) return Results.NotFound();

        await cache.SetAsync($"short:{shortCode}", original.OriginalUrl);
        _ = cache.IncrementAsync($"count:{shortCode}");

        return Results.Redirect(original.OriginalUrl);
    }

    private static async Task<IResult> GetAnalytics(string shortCode, IRedisCacheService cache, IShortLinkRepository repo)
    {
        var count = await cache.GetAsync<long?>($"count:{shortCode}") ?? 0;
        var link = await repo.GetByShortCodeAsync(shortCode);
        if (link == null) return Results.NotFound();

        return Results.Ok(new { shortCode, originalUrl = link.OriginalUrl, created = link.CreatedAt, clicks = count });
    }
}
