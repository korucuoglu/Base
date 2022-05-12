using AutoMapper;
using MediatR;
using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
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
        var product = await _unitOfWork.ProductReadRepository().FindAsync(_hashService.Decode(request.Id));

        var productDto = _mapper.Map<ProductDto>(product);

        return Response<ProductDto>.Success(productDto, 200);
    }
}