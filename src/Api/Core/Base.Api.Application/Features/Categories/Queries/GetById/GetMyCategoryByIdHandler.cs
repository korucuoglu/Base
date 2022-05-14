using AutoMapper;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Categories;

public class GetMyCategoryByIdHandler : IRequestHandler<GetMyCategoryById, Response<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly HashService _hashService;

    public GetMyCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashService = hashService;
        _identityService = identityService;
    }

    public async Task<Response<CategoryDto>> Handle(GetMyCategoryById request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.CategoryReadRepository().
            Where(x => x.Id == _hashService.Decode(request.Id) && x.ApplicationUserId == _identityService.GetUserDecodeId);

        var dto = await _mapper.ProjectTo<CategoryDto>(entity).FirstOrDefaultAsync();

        return Response<CategoryDto>.Success(dto, 200);
    }
}