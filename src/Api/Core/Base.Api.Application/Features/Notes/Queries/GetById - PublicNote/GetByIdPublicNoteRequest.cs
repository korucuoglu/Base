using Base.Api.Application.Models.Dtos;
using MediatR;

namespace Base.Api.Application.Features.Notes;

public class GetByIdPublicNoteRequest : IRequest<Response<PublicNoteDto>>
{
    public string Id { get; set; }
}