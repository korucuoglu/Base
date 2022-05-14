using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Features.Categories;

public class GetAllMyCategories : IRequest<Response<List<CategoryDto>>>
{
}