using AutoMapper;
using Base.Api.Application.Interfaces.UnitOfWork;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Models.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Notes;

public class GetAllPublicNotesHandler : IRequestHandler<GetAllPublicNotesRequest, Response<List<NoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllPublicNotesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<Response<List<NoteDto>>> Handle(GetAllPublicNotesRequest request, CancellationToken cancellationToken)
    {
        var query = @"SELECT id as ""Id"", title, content, users.""UserName"" AS username FROM notes
        JOIN ""AspNetUsers"" users ON users.""Id"" = notes.""ApplicationUserId"" WHERE notes.is_public = TRUE";

        var entities = _unitOfWork.NoteReadRepository().ExecuteQuery<NoteRawSqlQueryModel>(query);

        var dtos = _mapper.Map<List<NoteDto>>(entities);

        return Task.FromResult(Response<List<NoteDto>>.Success(data: dtos, statusCode: 200));
    }
}