using MediatR;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Dtos.Categories;
using Base.Api.Application.Services;

namespace Base.Api.Application.Features.Queries.Categories;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryById, Response<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HashService _hashService;
    private readonly IIdentityService _identityService;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork, HashService hashService, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _identityService = identityService;
    }

    public async Task<Response<NoContent>> Handle(DeleteCategoryById request, CancellationToken cancellationToken)
    {
        await _unitOfWork.CategoryWriteRepository()
            .RemoveAsync(x => x.Id == _hashService.Decode(request.Id) && x.ApplicationUserId == _identityService.GetUserDecodeId);

        bool result = await _unitOfWork.SaveChangesAsync() > 0;

        if (!result)
        {
            return Response<NoContent>.Fail("hata meydana geldi", 500);
        }

        return Response<NoContent>.Success(204);
    }
}