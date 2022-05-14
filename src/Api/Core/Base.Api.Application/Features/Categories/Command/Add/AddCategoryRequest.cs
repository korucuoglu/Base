using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Features.Categories;

public class AddCategoryRequest : IRequest<Response<string>>
{
    public string Title { get; set; }
}