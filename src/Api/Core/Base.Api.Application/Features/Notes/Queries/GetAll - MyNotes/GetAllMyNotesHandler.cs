using AutoMapper;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class GetAllMyNotesHandler : IRequestHandler<GetAllMyNotesRequest, Response<List<NoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetAllMyNotesHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Response<List<NoteDto>>> Handle(GetAllMyNotesRequest request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.NoteReadRepository().Where(x => x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dtos = await _mapper.ProjectTo<NoteDto>(entities).ToListAsync();

        return Response<List<NoteDto>>.Success(data: dtos, statusCode: 200);
    }
}