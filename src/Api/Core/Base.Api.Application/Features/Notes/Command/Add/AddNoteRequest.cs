using Base.Api.Application.Dtos.Wrappers;
using MediatR;

namespace Base.Api.Application.Features.Notes;

public class AddNoteRequest : IRequest<Response<string>>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string CategoryId { get; set; }
}