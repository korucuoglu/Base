using Base.Api.Application.Models.Dtos;
using MediatR;

namespace Base.Api.Application.Features.Notes;

public class GetByIdNoteRequest : IRequest<Response<NoteDto>>
{
    public string Id { get; set; }
}