using AutoMapper;
using Entities.Models;
using Interfaces.DTOs;


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
