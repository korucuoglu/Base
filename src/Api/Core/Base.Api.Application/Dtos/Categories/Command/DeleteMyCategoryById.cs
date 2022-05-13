using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Dtos.Categories;

public class DeleteMyCategoryById : IRequest<Response<NoContent>>
{
    public string Id { get; set; }
}