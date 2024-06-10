using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _service;
        private ILogger<ShortUrlController> _logger;

        public ShortUrlController(IShortUrlService service, ILogger<ShortUrlController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortUrl([FromBody] PostURL request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shortenedUrl = await _service.ShortenUrl(request.originalUrl, request.ExpiryInDays);
            return Ok(shortenedUrl);
        }

        [HttpGet("rd/{shortenedUrl}")]
        public async Task<IActionResult> GetOriginalUrlRedirect(string shortenedUrl)
        {
            var or = await _service.GetOriginalUrl(shortenedUrl);
            if (or == null)
            {
                return NotFound();
            }
            return Redirect($"https://{or}");
        }

        [HttpGet("{shortenedUrl}")]
        public async Task<IActionResult> GetOriginalUrl(string shortenedUrl)
        {
            var originalUrl = await _service.GetOriginalUrl(shortenedUrl);
            if (originalUrl == null)
            {
                return NotFound();
            }
            return Ok(originalUrl);
        }
    }
}
