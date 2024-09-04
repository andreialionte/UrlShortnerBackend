using Microsoft.AspNetCore.Mvc;
using UrlShortner.Services;

[Route("")]
[ApiController]
/*[EnableCors("DevCors")]*/
public class UrlShortnerController : ControllerBase
{
    private readonly IShortUrlService _shortUrlService;

    public UrlShortnerController(IShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
    }

    [HttpPost("ShortenUrl")]
    public async Task<IActionResult> ShortenUrl([FromQuery] string Url)
    {
        if (string.IsNullOrEmpty(Url))
        {
            return BadRequest("URL cannot be empty.");
        }

        try
        {
            var shortUrl = await _shortUrlService.ShortingTheUrl(Url);
            return Ok(shortUrl);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "error occurred while shortening the URL.");
        }
    }



    [HttpGet("GetLink")]
    public async Task<IActionResult> GetLink(string pathShortUrl)
    {
        if (string.IsNullOrEmpty(pathShortUrl))
        {
            return BadRequest("Path for short URL cannot be empty.");
        }

        var link = $"https://rourlsapi.hopto.org/{pathShortUrl}";
        return Ok(link);
    }

    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> GetTheShortUrl(string shortUrl)
    {
        if (string.IsNullOrEmpty(shortUrl))
        {
            return BadRequest("Short URL cannot be empty.");
        }

        try
        {
            var originalUrl = await _shortUrlService.GetUrl(shortUrl);

            if (string.IsNullOrEmpty(originalUrl))
            {
                return NotFound("The short URL was not found.");
            }

            if (!Uri.IsWellFormedUriString(originalUrl, UriKind.Absolute))
            {
                return StatusCode(500, "server error. The URL format is invalid.");
            }

            return Redirect(originalUrl);
        }
        catch (Exception ex)
        {
            return StatusCode(500, " error occurred while processing the short URL.");
        }
    }


}







// ideea after that u can create an leadarbord with the most shorten url in the app.