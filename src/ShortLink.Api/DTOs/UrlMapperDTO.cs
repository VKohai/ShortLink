namespace ShortLink.Api.DTOs;

public class UrlMapperDTO
{
    public required string Guid { get; set; }
    public required string OriginalUrl { get; set; }
    public required string? ShortUrl { get; set; }
    public uint Views { get; set; } = 0;
}
