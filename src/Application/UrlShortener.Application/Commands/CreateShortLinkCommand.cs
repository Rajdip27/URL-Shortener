using MediatR;
using UrlShortener.Application.Dtos;

namespace UrlShortener.Application.Commands;

public class CreateShortLinkCommand: IRequest<ShortLinkDto>
{
    public string OriginalUrl { get; set; } = null!;
    public string? CustomAlias { get; set; }
}