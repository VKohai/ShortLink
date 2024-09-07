namespace ShortLink.Shared.Models;

public class UrlMapper
{
    public required string OriginalUrl { get; set; }
    public required string Guid { get; set; }
    public required uint Views { get; set; }
}
