using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Features.Notes;
using Base.Api.Domain.Entities;
using Base.Api.Infrastructure.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Base.Api.Controllers;

[Authorize]
public class NotesController : BaseApiController
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMyNotes()
    {
        var data = await _mediator.Send(new GetAllMyNotesRequest());

        return Result(data);
    }

    [HttpGet("public")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPublicNotes()
    {
        var data = await _mediator.Send(new GetAllPublicNotesRequest());

        return Result(data);
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Note>))]
    public async Task<IActionResult> GetById(string id)
    {
        var data = await _mediator.Send(new GetMyNoteByIdRequest() { Id = id });

        return Result(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddNoteRequest dto)
    {
        var data = await _mediator.Send(dto);
        return Result(data);
    }

    [HttpPut]
    [ServiceFilter(typeof(NotFoundFilterAttribute<Note>))]
    public async Task<IActionResult> Update(UpdateNoteRequest dto)
    {
        var data = await _mediator.Send(dto);
        return Result(data);
    }

    [ServiceFilter(typeof(NotFoundFilterAttribute<Note>))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _mediator.Send(new DeleteMyNoteByIdRequest() { Id = id });
        return Result(data);
    }
}