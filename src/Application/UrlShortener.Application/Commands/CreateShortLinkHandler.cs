using MediatR;
using UrlShortener.Application.Dtos;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Infrastructure.Utilities;

namespace UrlShortener.Application.Commands;

public class CreateShortLinkHandler(IShortLinkRepository repo, Base62Encoder encoder) : IRequestHandler<CreateShortLinkCommand, ShortLinkDto>
{
    public async Task<ShortLinkDto> Handle(CreateShortLinkCommand request, CancellationToken cancellationToken)
    {
        // If original URL already exists, return existing record
        var existing = await repo.GetByOriginalUrlAsync(request.OriginalUrl);
        if (existing != null) return new ShortLinkDto(existing.ShortCode, existing.OriginalUrl, existing.CreatedAt);
        
        var id = Guid.NewGuid();
        var shortCode = request.CustomAlias;
        if (string.IsNullOrWhiteSpace(shortCode))
        {
            // Use Base62 encode of Guid bytes (trim to length)
            shortCode = encoder.Encode(id);
        }

        var link = new ShortLink
        {
            Id = id,
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode,
            CustomAlias = request.CustomAlias
        };

        await repo.AddAsync(link);
        return new ShortLinkDto(link.ShortCode, link.OriginalUrl, link.CreatedAt);
    }
}

