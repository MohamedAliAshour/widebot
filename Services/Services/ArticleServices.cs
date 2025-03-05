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


        public async Task<SaveArticlesViewModel> GetDetailsById(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return null;

            return new SaveArticlesViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PublishedDate,
                Tags = article.Tags
            };
        }

        public async Task<SaveArticlesViewModel> Add(SaveArticlesViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                PublishedDate = model.PublishedDate,
                Tags = model.Tags
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            model.Id = article.Id;
            return model;
        }

        public async Task<bool> Update(SaveArticlesViewModel model, int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            article.Title = model.Title;
            article.Content = model.Content;
            article.PublishedDate = model.PublishedDate;
            article.Tags = model.Tags;

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
