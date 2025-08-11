using FluentValidation;
using UrlShortener.Application.Commands;

namespace UrlShortener.Application.Validators;

public class CreateShortLinkValidator: AbstractValidator<CreateShortLinkCommand>
{
    public CreateShortLinkValidator()
    {
        RuleFor(x => x.OriginalUrl)
        .NotEmpty()
        .Must(u => Uri.TryCreate(u, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        .WithMessage("OriginalUrl must be a valid absolute http or https URL.");

        RuleFor(x => x.CustomAlias)
            .Matches("^[a-zA-Z0-9_-]{3,60}$")
            .When(x => !string.IsNullOrWhiteSpace(x.CustomAlias))
            .WithMessage("Custom alias may only contain letters, numbers, underscore and hyphen (3-60 chars).");
    }
}

