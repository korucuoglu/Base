using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Services;
using Base.Api.Application.Dtos.Notes;

namespace Base.Api.Application.Features.Queries.Notes;

public class GetAllNotesHandler : IRequestHandler<GetAllNotes, Response<List<NoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetAllNotesHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Response<List<NoteDto>>> Handle(GetAllNotes request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.NoteReadRepository().Where(x => x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dtos = await _mapper.ProjectTo<NoteDto>(entities).ToListAsync();

        return Response<List<NoteDto>>.Success(data: dtos, statusCode: 200);
    }
}