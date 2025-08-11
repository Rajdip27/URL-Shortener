using MediatR;
using UrlShortener.Application.Dtos;

namespace UrlShortener.Application.Queries;

public class GetOriginalUrlQuery : IRequest<ShortLinkDto?>
{
    public string ShortCode { get; set; } = null!;
}

