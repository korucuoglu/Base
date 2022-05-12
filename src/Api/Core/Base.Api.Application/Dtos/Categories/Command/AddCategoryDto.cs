using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Dtos.Categories;

public class AddCategoryDto : IRequest<Response<string>>
{
    public string Title { get; set; }
}