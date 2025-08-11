namespace UrlShortener.Application.Dtos;
public record ShortLinkDto(string ShortCode, string OriginalUrl, DateTimeOffset CreatedAt);
