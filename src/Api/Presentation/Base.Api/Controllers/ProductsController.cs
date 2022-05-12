using Base.Api.Application.Dtos.Products;
using Base.Api.Domain.Entities;
using Base.Api.Infrastructure.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Base.Api.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _mediator.Send(new GetAllProduct());

        return Result(data);
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Product>))]
    public async Task<IActionResult> GetById(string id)
    {
        var data = await _mediator.Send(new GetProductById() { Id = id });

        return Result(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProductDto dto)
    {
        var data = await _mediator.Send(dto);
        return Result(data);
    }

    [HttpPut]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Product>))]
    public async Task<IActionResult> Update(UpdateProductDto dto)
    {
        var data = await _mediator.Send(dto);
        return Result(data);
    }

    [ServiceFilter(typeof(NotFoundFilterAttribute<Product>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _mediator.Send(new DeleteProductById() { Id = id });
        return Result(data);
    }
}