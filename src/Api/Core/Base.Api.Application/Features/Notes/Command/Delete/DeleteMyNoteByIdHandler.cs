using Base.Api.Application.Interfaces.Services;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class DeleteMyNoteByIdHandler : IRequestHandler<DeleteMyNoteByIdRequest, Response<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly HashService _hashService;
    private readonly IIdentityService _identityService;

    public DeleteMyNoteByIdHandler(IUnitOfWork unitOfWork, HashService hashService, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _identityService = identityService;
    }

    public async Task<Response<NoContent>> Handle(DeleteMyNoteByIdRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.NoteWriteRepository()
            .RemoveAsync(x => x.Id == _hashService.Decode(request.Id) && x.ApplicationUserId == _identityService.GetUserDecodeId);

        bool result = await _unitOfWork.SaveChangesAsync() > 0;

        if (!result)
        {
            return Response<NoContent>.Fail("hata meydana geldi", 500);
        }

        return Response<NoContent>.Success(204);
    }
}