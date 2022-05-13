using AutoMapper;
using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Notes;

public class GetAllPublicNotesHandler : IRequestHandler<GetAllPublicNotes, Response<List<PublicNoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPublicNotesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<List<PublicNoteDto>>> Handle(GetAllPublicNotes request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.NoteReadRepository().Where(x => x.IsPublic);

        var dtos = await _mapper.ProjectTo<PublicNoteDto>(entities).ToListAsync();

        return Response<List<PublicNoteDto>>.Success(data: dtos, statusCode: 200);
    }
}