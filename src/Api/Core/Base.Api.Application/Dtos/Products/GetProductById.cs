using MediatR;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Application.Dtos.Products;

public class GetProductById : IRequest<Response<ProductDto>>
{
    public string Id { get; set; }
}