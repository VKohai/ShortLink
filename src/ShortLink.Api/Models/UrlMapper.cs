namespace ShortLink.Api.Models;

public class UrlMapper
{
    public required string Guid { get; set; }
    public required string OriginalUrl { get; set; }
    public uint Views { get; set; } = 0;
}
