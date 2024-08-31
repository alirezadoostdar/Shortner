using Microsoft.AspNetCore.Mvc;
using ShortnerUrl.Services;

namespace ShortnerUrl.Endpoints;

public static class ShortnerEndpoint
{
    public static void MapShortenEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/Shorten", async (
            ShortenService service,
            CancellationToken cancellationToken,
            [FromQuery(Name ="long)url")] string url) =>
        {
            // validation : xss attack
            var shortenUrl = await service.ShortenUrlAsync(url, cancellationToken);
            return Results.Ok(shortenUrl);
        });
    }
}
