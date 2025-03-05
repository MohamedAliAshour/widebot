using Entities.Models;
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

            var shortUrl = new ShortUrl
            {
                LongUrl = model.LongUrl,
                ShortCode = shortCode, // Save only the shortcode separately
                ShortUrl1 = fullShortUrl, // Save full short URL
                CreatedAt = DateTime.UtcNow
            };

            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();

            // Return the newly created data with the full shortened URL
            return new SaveShortUrlViewModel
            {
                LongUrl = shortUrl.LongUrl,
                ShortCode = shortUrl.ShortCode, // Include the short code in response
                ShortUrl1 = shortUrl.ShortUrl1 // Full shortened URL
            };
        }

        public async Task<List<GetShortUrlViewModel>> GetAll()
        {
            return await _context.ShortUrls.Select(s => new GetShortUrlViewModel
            {
                Id = s.Id,
                LongUrl = s.LongUrl,
                ShortCode = s.ShortCode, // Include short code
                ShortUrl1 = s.ShortUrl1, // Full shortened URL
                CreatedAt = s.CreatedAt
            }).ToListAsync();
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
