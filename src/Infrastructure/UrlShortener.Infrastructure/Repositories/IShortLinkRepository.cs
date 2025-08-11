using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Persistence;

namespace UrlShortener.Infrastructure.Repositories;

public interface IShortLinkRepository
{
    Task<ShortLink?> GetByShortCodeAsync(string shortCode);
    Task<ShortLink?> GetByOriginalUrlAsync(string originalUrl);
    Task AddAsync(ShortLink link);
}
public class ShortLinkRepository : IShortLinkRepository
{
    /// <summary>
    /// The database
    /// </summary>
    private readonly ShortenerDbContext _db;
    /// <summary>
    /// Initializes a new instance of the <see cref="ShortLinkRepository"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public ShortLinkRepository(ShortenerDbContext db) { _db = db; }
    /// <summary>
    /// Gets the by short code asynchronous.
    /// </summary>
    /// <param name="shortCode">The short code.</param>
    /// <returns></returns>
    public async Task<ShortLink?> GetByShortCodeAsync(string shortCode)
    {
        return await _db.ShortLinks.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
    }
    /// <summary>
    /// Gets the by original URL asynchronous.
    /// </summary>
    /// <param name="originalUrl">The original URL.</param>
    /// <returns></returns>
    public async Task<ShortLink?> GetByOriginalUrlAsync(string originalUrl)
    {
        return await _db.ShortLinks.FirstOrDefaultAsync(s => s.OriginalUrl == originalUrl);
    }

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="link">The link.</param>
    public async Task AddAsync(ShortLink link)
    {
        await _db.ShortLinks.AddAsync(link);
        await _db.SaveChangesAsync();
    }
}