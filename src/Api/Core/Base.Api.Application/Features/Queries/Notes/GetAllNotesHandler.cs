﻿using AutoMapper;
using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Notes;

public class GetNotesByCategoryIdHandler : IRequestHandler<GetNotesByCategoryId, Response<List<NoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly HashService _hashService;

    public GetNotesByCategoryIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService, HashService hashService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
        _hashService = hashService;
    }

    public async Task<Response<List<NoteDto>>> Handle(GetNotesByCategoryId request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.NoteReadRepository().
            Where(x => x.ApplicationUserId == _identityService.GetUserDecodeId && x.CategoryId == _hashService.Decode(request.Id));

        var dtos = await _mapper.ProjectTo<NoteDto>(entities).ToListAsync();

        return Response<List<NoteDto>>.Success(data: dtos, statusCode: 200);
    }
}