using Microsoft.AspNetCore.Mvc;
using widebot.DTOs;
using widebot.interfaces;

namespace widebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrl _shortUrlService;

        public ShortUrlController(IShortUrl shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateShortenUrl([FromBody] ShortUrlDto model)
        {
            if (string.IsNullOrWhiteSpace(model.LongUrl))
            {
                return BadRequest(new { Message = "Long URL is required." });
            }

            var result = await _shortUrlService.Add(model);

            return Ok(new
            {
                LongUrl = result.LongUrl,
                ShortenedUrl = result.ShortUrl1,
                ShortCode = result.ShortCode
            });
        }


        [HttpGet("redirect")]
        public async Task<IActionResult> RedirectToLongUrl([FromQuery] string shortenUrl)
        {
            var decodedUrl = Uri.UnescapeDataString(shortenUrl);
            var result = await _shortUrlService.GetByShortenUrl(decodedUrl);
            if (result == null)
            {
                return NotFound(new { Message = "Short URL not found." });
            }

            return Redirect(result.LongUrl);
        }



        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllShortUrls()
        {
            var result = await _shortUrlService.GetAll();
            return Ok(result);
        }

        [HttpGet("get-by-short-url/{shortenUrl}")]
        public async Task<IActionResult> GetByShortenUrl(string shortenUrl)
        {
            var decodedUrl = Uri.UnescapeDataString(shortenUrl);
            var result = await _shortUrlService.GetByShortenUrl(decodedUrl);

            if (result == null)
            {
                return NotFound(new { Message = "Short URL not found." });
            }

            return Ok(result);
        }

        [HttpGet("get-by-shortcode/{shortCode}")]
        public async Task<IActionResult> GetByShortCode(string shortCode)
        {
            var result = await _shortUrlService.GetByShortCode(shortCode);
            if (result == null)
            {
                return NotFound(new { Message = "Short code not found." });
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortUrl(int id)
        {
            var isDeleted = await _shortUrlService.Delete(id);
            if (!isDeleted)
            {
                return NotFound(new { Message = "Short URL not found." });
            }

            return Ok(new { Message = "Short URL deleted successfully." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShortUrlById(int id)
        {
            var shortUrl = await _shortUrlService.GetById(id);
            if (shortUrl == null)
            {
                return NotFound(new { Message = "Short URL not found." });
            }

            return Ok(shortUrl);
        }



    }
}
