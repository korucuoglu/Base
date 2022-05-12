using Base.Api.Application.Interfaces.Repositories;
using Base.Api.Domain.Entities;
using Base.Api.Persistence.Context;

namespace Base.Api.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}