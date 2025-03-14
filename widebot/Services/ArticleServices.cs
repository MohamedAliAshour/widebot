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

        public async Task<List<ArticlesDto>> GetAll()
        {
            var articles = await _context.Articles.ToListAsync();
            return _mapper.Map<List<ArticlesDto>>(articles);
        }

        public async Task<List<ArticlesDto>> GetWithFilltering(string filterOn, string filterQuery)
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
            return _mapper.Map<List<ArticlesDto>>(filteredArticles);
        }

        public async Task<PagedList<ArticlesDto>> GetArticlesWithPagination(int pageNumber, int pageSize)
        {
            var query = _context.Articles.AsQueryable();
            return await PagedList<ArticlesDto>.CreateAsync(
                query.Select(a => _mapper.Map<ArticlesDto>(a)),
                pageNumber,
                pageSize
            );
        }

        public async Task<ArticlesDto?> GetDetailsById(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            return article == null ? null : _mapper.Map<ArticlesDto>(article);
        }

        public async Task<ArticlesDto> Add(ArticlesDto model)
        {
            var article = _mapper.Map<Article>(model);
            article.PublishedDate = DateTime.UtcNow;

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArticlesDto>(article);
        }

        public async Task<bool> Update(ArticlesDto model, int id)
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
