using AutoMapper;
using MediatR;
using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Command.Products
{
    public class AddProductHandler : IRequestHandler<AddProductDto, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HashService _hashService;

        public AddProductHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<Response<string>> Handle(AddProductDto request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.ProductWriteRepository().AddAsync(_mapper.Map<Product>(request));

            var result = await _unitOfWork.SaveChangesAsync() > 0;

            if (!result)
            {
                return Response<string>.Fail("hata meydana geldi", 500);
            }

            return Response<string>.Success(data: _hashService.Encode(data.Id), 201);
        }
    }
}