using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Articles;

public class GetAllMyArticlesHandler : IRequestHandler<GetAllMyArticlesRequest, Response<List<ArticleDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetAllMyArticlesHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Response<List<ArticleDto>>> Handle(GetAllMyArticlesRequest request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.ArticleReadRepository().Where(x => x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dtos = await _mapper.ProjectTo<ArticleDto>(entities).ToListAsync();

        return Response<List<ArticleDto>>.Success(data: dtos, statusCode: 200);
    }
}