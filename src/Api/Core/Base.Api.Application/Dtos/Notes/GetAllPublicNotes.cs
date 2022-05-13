using Base.Api.Application.Dtos.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Dtos.Notes;

public class GetAllPublicNotes : IRequest<Response<List<PublicNoteDto>>>
{
}