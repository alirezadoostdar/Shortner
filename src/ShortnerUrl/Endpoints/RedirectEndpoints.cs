using Microsoft.AspNetCore.Mvc;
using ShortnerUrl.Services;

namespace ShortnerUrl.Endpoints;

public static class RedirectEndpoints
{
    public static void MapRedirectEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{shorten_Code}", async (ShortenService service,
            CancellationToken cancellationToken,
            [FromRoute(Name = "shorten_Code")] string shortenCode) =>
        {
            // 

            var longUrl = await service.GetLongUrl(shortenCode, cancellationToken);
            return Results.Redirect(longUrl);
        });
    }
}
