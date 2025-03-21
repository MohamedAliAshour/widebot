﻿using Microsoft.AspNetCore.Mvc;
using widebot.DTOs;
using widebot.interfaces;
using widebot.Validation;

namespace widebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlecs _articleService;

        public ArticlesController(IArticlecs articleService)
        {
            _articleService = articleService;
        }

        // Get all articles
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ArticlesGetDto>>> GetArticles()
        {
            var articles = await _articleService.GetAll();
            return Ok(articles);
        }

        // Get articles with filtering
        [HttpGet("GetWithFiltering")]
        public async Task<ActionResult<IEnumerable<ArticlesGetDto>>> GetArticlesWithFiltering(
            [FromQuery] string filterOn,
            [FromQuery] string filterQuery)
        {
            var articles = await _articleService.GetWithFilltering(filterOn, filterQuery);
            return Ok(articles);
        }

        // Get articles with pagination
        [HttpGet("GetWithPagination")]
        public async Task<ActionResult> GetArticlesWithPagination(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagedArticles = await _articleService.GetArticlesWithPagination(pageNumber, pageSize);

            return Ok(new
            {
                pagedArticles.CurrentPage,
                pagedArticles.TotalPages,
                pagedArticles.PageSize,
                pagedArticles.TotalCount,
                Data = pagedArticles
            });
        }

        // Get a single article by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticlesGetDto>> GetArticle(int id)
        {
            var article = await _articleService.GetDetailsById(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        // Create a new article
        [HttpPost]
        [ValidationModel]
        public async Task<ActionResult<ArticlesCreateDto>> CreateArticle([FromBody] ArticlesCreateDto model)
        {
            var createdArticle = await _articleService.Add(model);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
        }

        // Update an article
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticlesUpdateDto model)
        {
            if (id != model.Id)
                return BadRequest();

            var result = await _articleService.Update(model, id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // Delete an article
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
