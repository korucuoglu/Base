using MediatR;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Application.Dtos.Categories;

public class DeleteCategoryById : IRequest<Response<NoContent>>
{
    public string Id { get; set; }
}