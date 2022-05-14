using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Dtos.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Features.Notes;

public class GetAllPublicNotesRequest : IRequest<Response<List<PublicNoteDto>>>
{
}