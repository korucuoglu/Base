using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Dtos.Notes;

public class DeleteMyNoteById : IRequest<Response<NoContent>>
{
    public string Id { get; set; }
}