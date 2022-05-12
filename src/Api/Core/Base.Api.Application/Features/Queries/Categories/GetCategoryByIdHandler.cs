using AutoMapper;
using MediatR;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Base.Api.Application.Features.Queries.Categories;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Response<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly HashService _hashService;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashService = hashService;
        _identityService = identityService;
    }

    public async Task<Response<CategoryDto>> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.CategoryReadRepository().
            Where(x => x.Id == _hashService.Decode(request.Id) && x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dto = await _mapper.ProjectTo<CategoryDto>(entity).FirstOrDefaultAsync();

        return Response<CategoryDto>.Success(dto, 200);
    }
}