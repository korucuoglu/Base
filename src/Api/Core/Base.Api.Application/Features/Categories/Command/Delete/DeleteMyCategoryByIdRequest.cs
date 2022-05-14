using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Features.Categories;

public class DeleteMyCategoryByIdRequest : IRequest<Response<NoContent>>
{
    public string Id { get; set; }
}