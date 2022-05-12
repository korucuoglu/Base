using MediatR;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Application.Dtos.Notes;

public class GetNoteById : IRequest<Response<NoteDto>>
{
    public string Id { get; set; }
}