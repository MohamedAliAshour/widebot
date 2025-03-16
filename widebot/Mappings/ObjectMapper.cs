using AutoMapper;
using widebot.Models;
using widebot.DTOs;


namespace widebot.Mappings
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            #region Article
            CreateMap<Article, ArticlesCreateDto>().ReverseMap();
            CreateMap<Article, ArticlesUpdateDto>().ReverseMap();
            CreateMap<Article, ArticlesGetDto>().ReverseMap();
            CreateMap<Article, ArticlesDeleteDto>().ReverseMap();
            #endregion


            #region ShortUrl
            CreateMap<ShortUrl, ShortUrlCreateDto>().ReverseMap();
            CreateMap<ShortUrl, ShortUrlUpdateDto>().ReverseMap();
            CreateMap<ShortUrl, ShortUrlDeleteDto>().ReverseMap();
            CreateMap<ShortUrl, ShortUrlGetDto>().ReverseMap();
            #endregion
        }
    }
}
