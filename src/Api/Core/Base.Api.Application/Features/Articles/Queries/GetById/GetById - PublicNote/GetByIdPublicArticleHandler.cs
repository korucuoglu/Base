using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Models.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Articles;

public class GetByIdPublicArticleHandler : IRequestHandler<GetByIdPublicArticleRequest, Response<ArticleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdPublicArticleHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<Response<ArticleDto>> Handle(GetByIdPublicArticleRequest request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ArticleReadRepository();

        var query = $@"SELECT id AS ""Id"", title, content, is_public AS ""IsPublic"", created_date AS ""CreatedDate"", users.""UserName"" AS username FROM articles
        JOIN ""AspNetUsers"" users ON users.""Id"" = articles.""ApplicationUserId""
        WHERE articles.is_public = TRUE AND id = {request.Id}";

        var entity = repository.ExecuteQuery<ArticleRawSqlQueryModel>(query).First();

        var dto = _mapper.Map<ArticleDto>(entity);

        return Task.FromResult(Response<ArticleDto>.Success(data: dto, statusCode: 200));
    }
}