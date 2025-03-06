using Entities.Models;
using Interfaces.Helpers;
using Interfaces.interfaces;
using Interfaces.ViewModels.ShortUrlVM;
using Microsoft.EntityFrameworkCore;
using Services.Base;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ShortUrlServices : BaseService, IShortUrl
    {
        private readonly DataContext _context;
        private const string BaseUrl = "https://localhost:5176/"; // Replace with actual domain

        public ShortUrlServices(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SaveShortUrlViewModel> Add(SaveShortUrlViewModel model)
        {
            var shortCode = Guid.NewGuid().ToString("N").Substring(0, 6); // Generates a 6-character code
            var fullShortUrl = $"{BaseUrl}{shortCode}"; // Construct the full short URL

            var shortUrl = ObjectMapper.Mapper.Map<ShortUrl>(model);
            shortUrl.ShortCode = shortCode;
            shortUrl.ShortUrl1 = fullShortUrl;
            shortUrl.CreatedAt = DateTime.UtcNow;

            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();

            return ObjectMapper.Mapper.Map<SaveShortUrlViewModel>(shortUrl);
        }

        public async Task<List<GetShortUrlViewModel>> GetAll()
        {
            var shortUrls = await _context.ShortUrls.ToListAsync();
            return ObjectMapper.Mapper.Map<List<GetShortUrlViewModel>>(shortUrls);
        }

        public async Task<bool> Delete(int id)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.Id == id);
            if (shortUrl == null)
            {
                return false; // Record not found
            }

            _context.ShortUrls.Remove(shortUrl);
            await _context.SaveChangesAsync();
            return true; // Successfully deleted
        }

        public async Task<ShortUrl> GetById(int id)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ShortUrl> GetByShortenUrl(string shortenUrl)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortUrl1 == shortenUrl);
        }


        public async Task<ShortUrl> GetByShortCode(string shortCode)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        }
    }
}
