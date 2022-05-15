using AutoMapper;
using Base.Api.Application.Features.Categories;
using Base.Api.Application.Features.Articles;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Models.Entities;
using Base.Api.Domain.Entities;

namespace Base.Api.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        var hashService = new HashService();

        #region Categories

        CreateMap<Category, CategoryDto>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddCategoryRequest, Category>();

        CreateMap<UpdateCategoryRequest, Category>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)));

        #endregion Categories

        #region Notes

        CreateMap<Article, ArticleDto>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<ArticleRawSqlQueryModel, ArticleDto>().
              ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddArticleRequest, Article>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => hashService.Decode(src.CategoryId)));

        CreateMap<UpdateArticleRequest, Article>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => hashService.Decode(src.CategoryId)));

        #endregion Notes
    }
}