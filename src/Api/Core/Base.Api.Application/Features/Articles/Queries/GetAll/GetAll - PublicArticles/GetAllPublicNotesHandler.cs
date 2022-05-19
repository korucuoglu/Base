using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Articles;

public class GetAllPublicArticlesHandler : IRequestHandler<GetAllPublicArticlesRequest, Response<List<ArticleDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HashService _hashService;

    public GetAllPublicArticlesHandler(IUnitOfWork unitOfWork, HashService hashService)
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
    }

    public async Task<Response<List<ArticleDto>>> Handle(GetAllPublicArticlesRequest request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ArticleReadRepository();

        var entities = repository.Where(x => x.IsPublic);

        var dtos = await entities.Select(x => new ArticleDto()
        {
            Id = _hashService.Encode(x.Id),
            Username = x.ApplicationUser.UserName,
            Title = x.Title,
            Content = x.Content,
            CreatedDate = x.CreatedDate,
        }).ToListAsync();

        return Response<List<ArticleDto>>.Success(dtos, 200);
    }
}