using MediatR;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Application.Dtos.Categories;

public class GetCategoryById : IRequest<Response<CategoryDto>>
{
    public string Id { get; set; }
}