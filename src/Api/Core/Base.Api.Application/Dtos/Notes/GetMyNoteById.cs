using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Dtos.Notes;

public class GetMyNoteById : IRequest<Response<NoteDto>>
{
    public string Id { get; set; }
}