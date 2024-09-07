namespace ShortLink.Api.Services;

public static class ShortLinkService
{
    public static string GenerateShortUrl() =>
        Guid.NewGuid().ToString()[..8];
}
