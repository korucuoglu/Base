using AutoMapper;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Categories;

public class GetAllMyCategoriesHandler : IRequestHandler<GetAllMyCategories, Response<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetAllMyCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Response<List<CategoryDto>>> Handle(GetAllMyCategories request, CancellationToken cancellationToken)
    {
        var entities = _unitOfWork.CategoryReadRepository().Where(x => x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dtos = await _mapper.ProjectTo<CategoryDto>(entities).ToListAsync();

        return Response<List<CategoryDto>>.Success(data: dtos, statusCode: 200);
    }
}