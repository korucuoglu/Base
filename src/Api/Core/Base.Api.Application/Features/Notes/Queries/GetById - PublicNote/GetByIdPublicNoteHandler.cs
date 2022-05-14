using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Models.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class GetByIdPublicNoteHandler : IRequestHandler<GetByIdPublicNoteRequest, Response<NoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdPublicNoteHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<Response<NoteDto>> Handle(GetByIdPublicNoteRequest request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.NoteReadRepository();

        var query = @$"SELECT id as ""Id"", title, content, users.""UserName"" AS username FROM notes
        JOIN ""AspNetUsers"" users ON users.""Id"" = notes.""ApplicationUserId""
        WHERE notes.is_public = TRUE AND id={request.Id}";

        var entity = repository.ExecuteQuery<NoteRawSqlQueryModel>(query).First();

        var dto = _mapper.Map<NoteDto>(entity);

        return Task.FromResult(Response<NoteDto>.Success(data: dto, statusCode: 200));
    }
}