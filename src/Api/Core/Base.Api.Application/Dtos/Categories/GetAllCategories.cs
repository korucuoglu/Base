using MediatR;
using Base.Api.Application.Dtos.Wrappers;
using System.Collections.Generic;

namespace Base.Api.Application.Dtos.Categories;

public class GetAllCategories : IRequest<Response<List<CategoryDto>>>
{
}