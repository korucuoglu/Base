using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class UpdateNoteHandler : IRequestHandler<UpdateNoteRequest, Response<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateNoteHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<NoContent>> Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Note>(request);

        _unitOfWork.NoteWriteRepository().Update(entity);

        bool result = await _unitOfWork.SaveChangesAsync() > 0;

        if (!result)
        {
            return Response<NoContent>.Fail("hata meydana geldi", 500);
        }

        return Response<NoContent>.Success(204);
    }
}