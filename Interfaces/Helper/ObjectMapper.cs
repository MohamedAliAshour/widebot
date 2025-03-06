using AutoMapper;
using Entities.Models;
using Interfaces.ViewModels.ArticleVM;
using Interfaces.ViewModels.ShortUrlVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interfaces.Helpers
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<mapperProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class mapperProfile : Profile
    {

        public mapperProfile()
        {

            //CreateMap<decimal, decimal>().ConvertUsing(x => Math.Round(x, 5));

            //#region AspNetRole
            //CreateMap<AspNetRole, GetAspnetrolesViewModel>()
            //     .ForMember(dest => dest.AspNetRoleClaims, opt => opt.MapFrom(src => src.AspNetRoleClaims))
            //     .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
            //    .ReverseMap();
            //CreateMap<AspNetRole, GetAspnetrolessWithPaginationViewModel>().ReverseMap();
            //CreateMap<AspNetRole, SaveAspnetrolesViewModel>().ReverseMap();
            //#endregion

            CreateMap<Article, GetArticlesViewModel>()
                .ReverseMap();
            CreateMap<Article, GetArticlesWithPaginationViewModel>().ReverseMap();
            CreateMap<Article, SaveArticlesViewModel>().ReverseMap();


            CreateMap<ShortUrl, GetShortUrlViewModel>()
              .ReverseMap();
            CreateMap<ShortUrl, GetShortUrlWithPaginationViewModel>().ReverseMap();
            CreateMap<ShortUrl, SaveShortUrlViewModel>().ReverseMap();


        }
    }
}
