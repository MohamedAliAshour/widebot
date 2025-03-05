using Interfaces.interfaces;
using Interfaces.ViewModels.ShortUrlVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

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
        public async Task<IActionResult> CreateShortenUrl([FromBody] SaveShortUrlViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LongUrl))
            {
                return BadRequest(new { Message = "Long URL is required." });
            }

            var result = await _shortUrlService.Add(model);

            return Ok(new
            {
                LongUrl = result.LongUrl,
                ShortenedUrl = result.ShortUrl1, // Return the full shortened URL
                ShortCode = result.ShortCode
            });
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
            var decodedUrl = Uri.UnescapeDataString(shortenUrl); // Decode the URL
            var result = await _shortUrlService.GetByShortenUrl(decodedUrl);

            if (result == null)
            {
                return NotFound(new { Message = "Short URL not found." });
            }

            return Ok(new
            {
                Id = result.Id,
                LongUrl = result.LongUrl,
                ShortenedUrl = result.ShortUrl1,
                CreatedAt = result.CreatedAt
            });
        }




        [HttpGet("get-by-shortcode/{shortCode}")]
        public async Task<IActionResult> GetByShortCode(string shortCode)
        {
            var result = await _shortUrlService.GetByShortCode(shortCode);
            if (result == null)
            {
                return NotFound(new { Message = "Short code not found." });
            }
            return Ok(new
            {
                LongUrl = result.LongUrl,
                ShortenedUrl = result.ShortUrl1
            });
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
