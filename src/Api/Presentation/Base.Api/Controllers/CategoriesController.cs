using Base.Api.Application.Dtos.Categories;
using Base.Api.Domain.Entities;
using Base.Api.Infrastructure.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Base.Api.Controllers;

[Authorize]
public class CategoriesController : BaseApiController
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _mediator.Send(new GetAllCategories());

        return Result(data);
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Category>))]
    public async Task<IActionResult> GetById(string id)
    {
        var data = await _mediator.Send(new GetCategoryById() { Id = id });

        return Result(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCategoryDto dto)
    {
        var data = await _mediator.Send(dto);
        return Result(data);
    }

    [HttpPut]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Category>))]
    public async Task<IActionResult> Update(UpdateCategoryDto dto)
    {
        var data = await _mediator.Send(dto);
        return Result(data);
    }

    [ServiceFilter(typeof(NotFoundFilterAttribute<Category>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _mediator.Send(new DeleteCategoryById() { Id = id });
        return Result(data);
    }
}
