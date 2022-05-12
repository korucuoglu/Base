using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Dtos.Products;

public class DeleteProductById : IRequest<Response<NoContent>>
{
    public string Id { get; set; }
}