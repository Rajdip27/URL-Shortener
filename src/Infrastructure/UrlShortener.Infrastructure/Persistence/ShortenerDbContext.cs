using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Persistence;

public class ShortenerDbContext:DbContext
{
    public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) : base(options) { }

    public DbSet<ShortLink> ShortLinks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortLink>().HasIndex(s => s.ShortCode).IsUnique();
    }
}
