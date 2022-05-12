using MediatR;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Application.Dtos.Products;

public class AddProductDto : IRequest<Response<string>>
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Image { get; set; }
    public string Url { get; set; }
}