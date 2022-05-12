using Base.Api.Application.Interfaces.Repositories;
using Base.Api.Domain.Entities;
using Base.Api.Persistence.Context;

namespace Base.Api.Persistence.Repositories;

public class NoteWriteRepository : WriteRepository<Note>, INoteWriteRepository
{
    public NoteWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}