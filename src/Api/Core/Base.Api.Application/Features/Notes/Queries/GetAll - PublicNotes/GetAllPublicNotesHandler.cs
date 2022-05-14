using AutoMapper;
using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class GetAllPublicNotesHandler : IRequestHandler<GetAllPublicNotesRequest, Response<List<PublicNoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPublicNotesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<Response<List<PublicNoteDto>>> Handle(GetAllPublicNotesRequest request, CancellationToken cancellationToken)
    {
        var query = @"SELECT id, title, content, users.""UserName"" AS username FROM notes
        JOIN ""AspNetUsers"" users ON users.""Id"" = notes.""ApplicationUserId"" WHERE notes.is_public = TRUE";

        var entities = _unitOfWork.NoteReadRepository().ExecuteQuery<PublicNoteEntity>(query);

        var dtos = _mapper.Map<List<PublicNoteDto>>(entities);

        return Task.FromResult(Response<List<PublicNoteDto>>.Success(data: dtos, statusCode: 200));
    }
}