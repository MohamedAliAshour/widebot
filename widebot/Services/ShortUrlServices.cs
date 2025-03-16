using AutoMapper;
using Microsoft.EntityFrameworkCore;
using widebot.DTOs;
using widebot.interfaces;
using widebot.Models;


public class ShortUrlServices : IShortUrl
{
    private readonly WidebotContext _context;
    private readonly IMapper _mapper;
    private const string BaseUrl = "http://localhost:5176/"; // Replace with actual domain

    // Inject IMapper through the constructor
    public ShortUrlServices(WidebotContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShortUrlCreateDto> Add(ShortUrlCreateDto model)
    {
        var shortCode = Guid.NewGuid().ToString("N"); // Generates a 6-character code
        var fullShortUrl = $"{BaseUrl}{shortCode}"; // Construct the full short URL

        var shortUrl = _mapper.Map<ShortUrl>(model);
        shortUrl.ShortCode = shortCode;
        shortUrl.ShortUrl1 = fullShortUrl;
        shortUrl.CreatedAt = DateTime.UtcNow;

        _context.ShortUrls.Add(shortUrl);
        await _context.SaveChangesAsync();

        return _mapper.Map<ShortUrlCreateDto>(shortUrl);
    }

    public async Task<List<ShortUrlGetDto>> GetAll()
    {
        var shortUrls = await _context.ShortUrls.ToListAsync();
        return _mapper.Map<List<ShortUrlGetDto>>(shortUrls);
    }

    public async Task<bool> Delete(int id)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.Id == id);
        if (shortUrl == null)
        {
            return false;
        }

        _context.ShortUrls.Remove(shortUrl);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ShortUrlGetDto> GetById(int id)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.Id == id);
        return shortUrl == null ? null : _mapper.Map<ShortUrlGetDto>(shortUrl);
    }

    public async Task<ShortUrlGetDto> GetByShortenUrl(string shortenUrl)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortUrl1 == shortenUrl);
        return shortUrl == null ? null : _mapper.Map<ShortUrlGetDto>(shortUrl);
    }

    public async Task<ShortUrlGetDto> GetByShortCode(string shortCode)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        return shortUrl == null ? null : _mapper.Map<ShortUrlGetDto>(shortUrl);
    }
}
