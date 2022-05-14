using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Features.Categories;

public class GetMyCategoryById : IRequest<Response<CategoryDto>>
{
    public string Id { get; set; }
}