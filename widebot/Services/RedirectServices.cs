using AutoMapper;
using Microsoft.EntityFrameworkCore;
using widebot.DTOs;
using widebot.interfaces;
using widebot.Models;


public class RedirectServices : IRedirect
{
    private readonly WidebotContext _context;
    private readonly IMapper _mapper;

    public RedirectServices(WidebotContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
  
    public async Task<ShortUrlGetDto> GetByShortCode(string shortCode)
    {
        var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        return shortUrl == null ? null : _mapper.Map<ShortUrlGetDto>(shortUrl);
    }
}
