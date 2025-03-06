using Interfaces.Helpers;
using Interfaces.interfaces;
using Interfaces.ViewModels.ArticleVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Model;
using Services.Services;


namespace widebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlecs _articleService;
        private readonly DataContext _context;

        public ArticlesController(DataContext context, IArticlecs articleService)
        {
            _context = context;
            _articleService = articleService;
        }

        // Get all articles
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetArticlesViewModel>>> GetArticles()
        {
            var articles = await _articleService.GetAll();
            return Ok(articles);

        }

        // Get all articles With Fillter
        [HttpGet("GetWithFiltering")]
        public async Task<ActionResult<IEnumerable<GetArticlesViewModel>>> GetArticlesWithFilltering([FromQuery] string? fillterOn, [FromQuery] string? fillterQuery)
        {
            var articles = await _articleService.GetWithFilltering(fillterOn, fillterQuery);
            return Ok(articles);

        }

        // Get a single article by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<SaveArticlesViewModel>> GetArticle(int id)
        {
            var article = await _articleService.GetDetailsById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // Create a new article
        [HttpPost]
        public async Task<ActionResult<SaveArticlesViewModel>> CreateArticle(SaveArticlesViewModel model)
        {
            var createdArticle = await _articleService.Add(model);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);

        }

        // Update an article
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, SaveArticlesViewModel model)
        {

            if (id != model.Id)
            {
                return BadRequest();
            }

            var result = await _articleService.Update(model, id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete an article
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.Delete(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
