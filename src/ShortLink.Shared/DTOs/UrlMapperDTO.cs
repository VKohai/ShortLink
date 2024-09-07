namespace ShortLink.Shared.DTOs;

public class UrlMapperDTO
{
    public required string OriginalUrl { get; set; }
    public required string Guid { get; set; }
    public required uint Views { get; set; }
    public string? ShortUrl { get; set; }
}
