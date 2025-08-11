using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Entities;

public class ShortLink
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string OriginalUrl { get; set; } = null!;

    [Required]
    public string ShortCode { get; set; } = null!;

    public string? CustomAlias { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    public int? OwnerId { get; set; } // optional
}
