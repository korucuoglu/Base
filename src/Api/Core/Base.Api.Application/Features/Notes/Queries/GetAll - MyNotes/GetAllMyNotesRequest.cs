using Base.Api.Application.Models.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Features.Notes;

public class GetAllMyNotesRequest : IRequest<Response<List<NoteDto>>>
{
}