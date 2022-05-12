using AutoMapper;
using MediatR;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Services;
using Microsoft.EntityFrameworkCore;
using Base.Api.Application.Dtos.Notes;

namespace Base.Api.Application.Features.Queries.Notes;

public class GetNoteByIdHandler : IRequestHandler<GetNoteById, Response<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly HashService _hashService;

    public GetNoteByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashService = hashService;
        _identityService = identityService;
    }

    public async Task<Response<NoteDto>> Handle(GetNoteById request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.NoteReadRepository().
            Where(x => x.Id == _hashService.Decode(request.Id) && x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dto = await _mapper.ProjectTo<NoteDto>(entity).FirstOrDefaultAsync();

        return Response<NoteDto>.Success(dto, 200);
    }
}