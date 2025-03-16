using AutoMapper;
using widebot.Models;
using Microsoft.EntityFrameworkCore;
using widebot.DTOs;
using widebot.interfaces;
using widebot.pagination;

namespace widebot.Services
{
    public class ArticleServices : IArticlecs
    {
        private readonly WidebotContext _context;
        private readonly IMapper _mapper;

        public ArticleServices(WidebotContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ArticlesGetDto>> GetAll()
        {
            var articles = await _context.Articles.ToListAsync();
            return _mapper.Map<List<ArticlesGetDto>>(articles);
        }

        public async Task<List<ArticlesGetDto>> GetWithFilltering(string filterOn, string filterQuery)
        {
            var query = _context.Articles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(a => a.Title.Contains(filterQuery));
                }
            }

            var filteredArticles = await query.ToListAsync();
            return _mapper.Map<List<ArticlesGetDto>>(filteredArticles);
        }

        public async Task<PagedList<ArticlesGetDto>> GetArticlesWithPagination(int pageNumber, int pageSize)
        {
            var query = _context.Articles.AsQueryable();
            return await PagedList<ArticlesGetDto>.CreateAsync(
                query.Select(a => _mapper.Map<ArticlesGetDto>(a)),
                pageNumber,
                pageSize
            );
        }

        public async Task<ArticlesGetDto?> GetDetailsById(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            return article == null ? null : _mapper.Map<ArticlesGetDto>(article);
        }

        public async Task<ArticlesCreateDto> Add(ArticlesCreateDto model)
        {
            var article = _mapper.Map<Article>(model);
            article.PublishedDate = DateTime.UtcNow;

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArticlesCreateDto>(article);
        }

        public async Task<bool> Update(ArticlesUpdateDto model, int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _mapper.Map(model, article);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }

        public Article? GetById(int id)
        {
            return _context.Articles.Find(id);
        }
    }
}
