using MediatR;
using Base.Api.Application.Dtos.Wrappers;

namespace Base.Api.Application.Dtos.Notes;

public class AddNoteDto : IRequest<Response<string>>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public string CategoryId { get; set; }
}