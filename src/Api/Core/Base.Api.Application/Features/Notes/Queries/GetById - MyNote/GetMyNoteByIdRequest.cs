using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Features.Notes;

public class GetMyNoteByIdRequest : IRequest<Response<NoteDto>>
{
    public string Id { get; set; }
}