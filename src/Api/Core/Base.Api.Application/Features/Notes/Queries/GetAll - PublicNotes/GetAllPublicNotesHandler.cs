using AutoMapper;
using Base.Api.Application.Models.Dtos;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.Api.Application.Models.Entities;

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
        var query = @"SELECT id as ""Id"", title, content, users.""UserName"" AS username FROM notes
        JOIN ""AspNetUsers"" users ON users.""Id"" = notes.""ApplicationUserId"" WHERE notes.is_public = TRUE";

        var entities = _unitOfWork.NoteReadRepository().ExecuteQuery<PublicNote>(query);

        var dtos = _mapper.Map<List<PublicNoteDto>>(entities);

        return Task.FromResult(Response<List<PublicNoteDto>>.Success(data: dtos, statusCode: 200));
    }
}