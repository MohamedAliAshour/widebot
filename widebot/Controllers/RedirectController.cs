using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using widebot.interfaces;

namespace widebot.Controllers
{
    [ApiController]
    [Route("")]
    public class RedirectController : ControllerBase
    {
        private readonly IRedirect _shortUrlService;

        public RedirectController(IRedirect shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortCode)
        {
            var result = await _shortUrlService.GetByShortCode(shortCode);
            if (result == null)
            {
                return NotFound(new { Message = "Short code not found." });
            }

            return Redirect(result.LongUrl);
        }
    }

}
