using AutoMapper;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Domain.Entities;

namespace Base.Api.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        var hashService = new HashService();

        #region Products

        CreateMap<Product, ProductDto>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddProductDto, Product>();

        CreateMap<UpdateProductDto, Product>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)));

        #endregion

        #region Categories

        CreateMap<Category, CategoryDto>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddCategoryDto, Category>();

        CreateMap<UpdateCategoryDto, Category>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)));

        #endregion
    }
}