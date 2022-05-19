using Base.Api.Application.Models.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Features.Articles;

public class GetAllPublicArticlesRequest : IRequest<Response<List<ArticleDto>>>
{
}