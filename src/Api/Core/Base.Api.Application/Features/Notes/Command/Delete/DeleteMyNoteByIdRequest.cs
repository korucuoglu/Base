using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Features.Notes;

public class DeleteMyNoteByIdRequest : IRequest<Response<NoContent>>
{
    public string Id { get; set; }
}