using AutoMapper;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class GetMyNoteByIdHandler : IRequestHandler<GetMyNoteByIdRequest, Response<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly HashService _hashService;

    public GetMyNoteByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashService = hashService;
        _identityService = identityService;
    }

    public async Task<Response<NoteDto>> Handle(GetMyNoteByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.NoteReadRepository().
            Where(x => x.Id == _hashService.Decode(request.Id) && x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dto = await _mapper.ProjectTo<NoteDto>(entity).FirstOrDefaultAsync();

        return Response<NoteDto>.Success(dto, 200);
    }
}