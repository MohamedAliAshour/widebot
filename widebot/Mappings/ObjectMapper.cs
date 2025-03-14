using AutoMapper;
using widebot.Models;
using widebot.DTOs;


namespace widebot.Mappings
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<Article, ArticlesDto>().ReverseMap();
            CreateMap<ShortUrl, ShortUrlDto>().ReverseMap();
        }
    }
}
