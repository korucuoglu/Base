using Base.Api.Application.Interfaces.Repositories;
using Base.Api.Domain.Entities;
using Base.Api.Persistence.Context;

namespace Base.Api.Persistence.Repositories;

public class NoteReadRepository : ReadRepository<Note>, INoteReadRepository
{
    public NoteReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}