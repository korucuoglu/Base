using AutoMapper;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Models.Entities;
using Base.Api.Application.Interfaces.Services;
using System.Linq;

namespace Base.Api.Application.Features.Notes;

public class GetByIdPublicNoteHandler : IRequestHandler<GetByIdPublicNoteRequest, Response<PublicNoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly HashService _hashService;

    public GetByIdPublicNoteHandler(IUnitOfWork unitOfWork, IMapper mapper, HashService hashService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashService = hashService;
    }

    public Task<Response<PublicNoteDto>> Handle(GetByIdPublicNoteRequest request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.NoteReadRepository();
        var decodeId = _hashService.Decode(request.Id);

        if (!repository.Any(x => x.Id == decodeId && x.IsPublic))
        {
            return Task.FromResult(Response<PublicNoteDto>.Fail("Not Found", 500));
        }

        var query = @$"SELECT id as ""Id"", title, content, users.""UserName"" AS username FROM notes
        JOIN ""AspNetUsers"" users ON users.""Id"" = notes.""ApplicationUserId""
        WHERE notes.is_public = TRUE AND id={decodeId}";

        var entities = _unitOfWork.NoteReadRepository().ExecuteQuery<PublicNote>(query).First();

        var dtos = _mapper.Map<PublicNoteDto>(entities);

        return Task.FromResult(Response<PublicNoteDto>.Success(data: dtos, statusCode: 200));
    }
}