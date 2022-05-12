using Base.Api.Application.Dtos.Products;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Products;

public class DeleteProductHandler : IRequestHandler<DeleteProductById, Response<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HashService _hashService;

    public DeleteProductHandler(IUnitOfWork unitOfWork, HashService hashService)
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
    }

    public async Task<Response<NoContent>> Handle(DeleteProductById request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProductWriteRepository().RemoveAsync(x => x.Id == _hashService.Decode(request.Id));

        bool result = await _unitOfWork.SaveChangesAsync() > 0;

        if (!result)
        {
            return Response<NoContent>.Fail("hata meydana geldi", 500);
        }

        return Response<NoContent>.Success(204);
    }
}