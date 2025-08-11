using MediatR;
using UrlShortener.Application.Dtos;
using UrlShortener.Infrastructure.Repositories;

namespace UrlShortener.Application.Queries;

public class GetOriginalUrlHandler : IRequestHandler<GetOriginalUrlQuery, ShortLinkDto?>
{
    private readonly IShortLinkRepository _repo;
    public GetOriginalUrlHandler(IShortLinkRepository repo) { _repo = repo; }

    public async Task<ShortLinkDto?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
    {
        var link = await _repo.GetByShortCodeAsync(request.ShortCode);
        if (link == null) return null;
        return new ShortLinkDto(link.ShortCode, link.OriginalUrl, link.CreatedAt);
    }
}
