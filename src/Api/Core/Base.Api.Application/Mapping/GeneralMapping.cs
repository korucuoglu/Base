using AutoMapper;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Notes;
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

        #endregion Products

        #region Categories

        CreateMap<Category, CategoryDto>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddCategoryDto, Category>();

        CreateMap<UpdateCategoryDto, Category>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)));

        #endregion Categories

        #region Notes

        CreateMap<Note, NoteDto>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddNoteDto, Note>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => hashService.Decode(src.CategoryId)));

        CreateMap<UpdateNoteDto, Note>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => hashService.Decode(src.CategoryId)));

        #endregion Notes
    }
}