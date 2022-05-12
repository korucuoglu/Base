using MediatR;
using Base.Api.Application.Dtos.Wrappers;
using System.Collections.Generic;

namespace Base.Api.Application.Dtos.Notes;

public class GetAllNotes : IRequest<Response<List<NoteDto>>>
{
}