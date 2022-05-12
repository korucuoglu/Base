using AutoMapper;
using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Products;

public class GetProductByIdHandler : IRequestHandler<GetProductById, Response<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly HashService _hashService;

    public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashService = hashService;
    }

    public async Task<Response<ProductDto>> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        var entity = _unitOfWork.ProductReadRepository().Where(x => x.Id == _hashService.Decode(request.Id));

        var dto = await _mapper.ProjectTo<ProductDto>(entity).FirstOrDefaultAsync();

        return Response<ProductDto>.Success(dto, 200);
    }
}