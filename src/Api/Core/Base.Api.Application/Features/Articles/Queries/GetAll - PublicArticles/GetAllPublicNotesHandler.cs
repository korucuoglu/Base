using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Models.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Articles;

public class GetAllPublicArticlesHandler : IRequestHandler<GetAllPublicArticlesRequest, Response<List<ArticleDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPublicArticlesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<Response<List<ArticleDto>>> Handle(GetAllPublicArticlesRequest request, CancellationToken cancellationToken)
    {
        var query = @"SELECT id AS ""Id"", title, content, is_public AS ""IsPublic"", created_date AS ""CreatedDate"", users.""UserName"" AS username FROM articles
        JOIN ""AspNetUsers"" users ON users.""Id"" = articles.""ApplicationUserId""
        WHERE articles.is_public = TRUE";

        var entities = _unitOfWork.ArticleReadRepository().ExecuteQuery<ArticleRawSqlQueryModel>(query);

        var dtos = _mapper.Map<List<ArticleDto>>(entities);

        return Task.FromResult(Response<List<ArticleDto>>.Success(data: dtos, statusCode: 200));
    }
}