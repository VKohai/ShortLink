using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShortLink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortLinkController(ShortLinkRepository repos) : ControllerBase
{
    private readonly ShortLinkRepository _repository = repos;

    /// <param name="guid">Path code of the link (without domain)</param>
    [HttpGet]
    [EnableCors("AnyOrigin")]
    public async Task<ActionResult<UrlMapper>> Get([FromQuery] string guid)
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
    [EnableCors("AnyOrigin")]
    public async Task<ActionResult<string?>> Post([FromBody] string link)
    {
        try
        {
            //Checks if the link is uri.
            var uri = new Uri(link);

            var urlMapper = new UrlMapper
            {
                OriginalUrl = link,
                Guid = ShortLinkService.GenerateShortUrl(),
                Views = 0
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
