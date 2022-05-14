using AutoMapper;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Entities.Notes;
using Base.Api.Application.Features.Categories;
using Base.Api.Application.Features.Notes;
using Base.Api.Application.Interfaces.Services;
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

        CreateMap<Note, NoteDto>().
               ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<PublicNote, PublicNoteDto>().
              ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Encode(src.Id)));

        CreateMap<AddNoteRequest, Note>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => hashService.Decode(src.CategoryId)));

        CreateMap<UpdateNoteRequest, Note>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => hashService.Decode(src.Id)))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => hashService.Decode(src.CategoryId)));

        #endregion Notes
    }
}