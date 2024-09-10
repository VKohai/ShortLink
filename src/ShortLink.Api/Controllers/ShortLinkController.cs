using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AnyOrigin")]
public class ShortLinkController(ShortLinkRepository repos) : ControllerBase
{
    private readonly ShortLinkRepository _repository = repos;

    /// <param name="guid">Path code of the link (without domain)</param>
    [HttpGet("{guid}")]
    public async Task<ActionResult<UrlMapperDTO>> Get(string guid)
    {
        try
        {
            var urlMapper = await _repository.GetByGuidAsync(guid);
            return urlMapper != null ?
                Ok(new UrlMapperDTO
                {
                    Guid = urlMapper.Guid,
                    OriginalUrl = urlMapper.OriginalUrl,
                    Views = urlMapper.Views,
                    ShortUrl = $"https://localhost:5200/{urlMapper.Guid}"
                }) :
                NotFound($"Not found: {guid}.\n" +
                $"Perhaps you sent me a link with domain, althougth you must send me code only.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<UrlMapperDTO?>> Post([FromBody] string link)
    {
        try
        {
            //Checks if the link is uri.
            var uri = new Uri(link);

            var urlMapper = new UrlMapper
            {
                OriginalUrl = link,
                Guid = ShortLinkService.GenerateShortUrl()
            };

            await _repository.AddAsync(urlMapper);

            return Ok(new UrlMapperDTO
            {
                Guid = urlMapper.Guid,
                OriginalUrl = urlMapper.OriginalUrl,
                Views = urlMapper.Views,
                ShortUrl = $"http://localhost:5000/{urlMapper.Guid}"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
