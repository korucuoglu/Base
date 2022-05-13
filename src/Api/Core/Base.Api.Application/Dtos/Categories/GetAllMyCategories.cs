using Base.Api.Application.Dtos.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Base.Api.Application.Dtos.Categories;

public class GetAllMyCategories : IRequest<Response<List<CategoryDto>>>
{
}