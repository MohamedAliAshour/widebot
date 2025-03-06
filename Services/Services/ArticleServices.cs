using Entities.Models;
using Interfaces.Base;
using Interfaces.Helpers;
using Interfaces.interfaces;
using Interfaces.ViewModels.ArticleVM;
using Microsoft.EntityFrameworkCore;
using Services.Base;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ArticleServices : BaseService, IArticlecs
    {
        private readonly DataContext _context;

        public ArticleServices(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GetArticlesViewModel>> GetAll()
        {
            return await _context.Articles.Select(a => new GetArticlesViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                PublishedDate = a.PublishedDate,
                Tags = a.Tags
            }).ToListAsync();
        }

        public async Task<List<GetArticlesViewModel>> GetWithFilltering(string? filterOn = null, string? filterQuery = null)
        {
            var articles = _context.Articles.Select(a => new GetArticlesViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                PublishedDate = a.PublishedDate,
                Tags = a.Tags
            }).AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    articles = articles.Where(a => a.Title.Contains(filterQuery));
                }
            }

            return await articles.ToListAsync();
        }



        public async Task<SaveArticlesViewModel> GetDetailsById(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return null;

            return ObjectMapper.Mapper.Map<SaveArticlesViewModel>(article);
        }

        public async Task<SaveArticlesViewModel> Add(SaveArticlesViewModel model)
        {
            var article = ObjectMapper.Mapper.Map<Article>(model);
            article.PublishedDate = DateTime.UtcNow;

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return ObjectMapper.Mapper.Map<SaveArticlesViewModel>(article);
        }

        public async Task<bool> Update(SaveArticlesViewModel model, int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            article = ObjectMapper.Mapper.Map(model, article);
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

        public async Task<bool> CheckNameExist(string name)
        {
            return await _context.Articles.AnyAsync(a => a.Title == name);
        }

        public Article GetById(int id)
        {
            return _context.Articles.Find(id);
        }
    }
}
