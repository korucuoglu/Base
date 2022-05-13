using Base.Api.Application.Dtos.Notes;
using Base.Api.Application.Dtos.Wrappers;
using Base.Api.Application.Interfaces.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Api.Application.Features.Queries.Notes;

public class GetAllPublicNotesHandler : IRequestHandler<GetAllPublicNotes, Response<List<PublicNoteDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPublicNotesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Response<List<PublicNoteDto>>> Handle(GetAllPublicNotes request, CancellationToken cancellationToken)
    {
        var query = @"SELECT id, title, content, users.""UserName"" AS username FROM notes 
        JOIN ""AspNetUsers"" users ON users.""Id"" = notes.""ApplicationUserId"" WHERE notes.is_public = TRUE";

        var dtos = _unitOfWork.NoteReadRepository().RawSqlQuery(query, x=> new PublicNoteDto { Title = (string)x[1], Content = (string)x[2], Username = (string)x[3] });

        return Task.FromResult(Response<List<PublicNoteDto>>.Success(data: dtos, statusCode: 200));
    }
}