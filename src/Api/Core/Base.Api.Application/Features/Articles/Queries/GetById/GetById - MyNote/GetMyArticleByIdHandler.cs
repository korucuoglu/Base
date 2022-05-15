using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Articles;

public class GetMyArticleByIdHandler : IRequestHandler<GetMyArticleByIdRequest, Response<ArticleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetMyArticleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Response<ArticleDto>> Handle(GetMyArticleByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.ArticleReadRepository().
            Where(x => x.Id == request.Id && x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dto = await _mapper.ProjectTo<ArticleDto>(entity).FirstOrDefaultAsync();

        return Response<ArticleDto>.Success(dto, 200);
    }
}